﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PKHeX
{
    public partial class SAV_PokedexORAS : Form
    {
        public SAV_PokedexORAS(Form1 frm1)
        {
            InitializeComponent();
            m_parent = frm1;
            Array.Copy(m_parent.savefile, sav, sav.Length);
            sv = m_parent.savindex * 0x7F000;
            dexoffset += sv;
            Setup();
            editing = false;
            LB_Species.SelectedIndex = 0;
            TB_Spinda.Text = BitConverter.ToUInt32(sav, dexoffset + 0x680).ToString("X8");
        }
        private int dexoffset = 0x15000 + 0x5400;
        Form1 m_parent;
        public byte[] sav = new Byte[0x100000];
        public int sv = 0;
        public bool[,] specbools = new bool[10, 0x60 * 8];
        public bool[,] langbools = new bool[7, 0x60 * 8];
        public bool[] foreignbools = new bool[0x52 * 8];
        bool editing = true;
        private void Setup()
        {
            // Clear Listbox and ComboBox
            try
            {
                LB_Species.Items.Clear();
                CB_Species.Items.Clear();
            }
            catch { }

            // Fill List
            #region Species
            {
                List<cbItem> species_list = new List<cbItem>();
                // Sort the Rest based on String Name
                string[] sortedspecies = new string[Form1.specieslist.Length];
                Array.Copy(Form1.specieslist, sortedspecies, Form1.specieslist.Length);
                Array.Sort(sortedspecies);

                // Add the rest of the items
                for (int i = 0; i < sortedspecies.Length; i++)
                {
                    cbItem ncbi = new cbItem();
                    ncbi.Text = sortedspecies[i];
                    ncbi.Value = Array.IndexOf(Form1.specieslist, sortedspecies[i]);
                    species_list.Add(ncbi);
                }
                species_list.RemoveAt(0); // Remove 0th Entry
                CB_Species.DisplayMember = "Text";
                CB_Species.ValueMember = "Value";
                CB_Species.DataSource = species_list;
            }
            #endregion

            for (int i = 1; i < Form1.specieslist.Length; i++)
                LB_Species.Items.Add(i.ToString("000") + " - " + Form1.specieslist[i]);
            for (int i = 722; i <= 0x300; i++)
                LB_Species.Items.Add(i.ToString("000") + " - ???");

            // Fill Bit arrays
            for (int i = 0; i < 0xA; i++)
            {
                byte[] data = new Byte[0x60];
                Array.Copy(sav, dexoffset +0x8 + 0x60 * i, data, 0, 0x60);
                BitArray BitRegion = new BitArray(data);
                for (int b = 0; b < (0x60 * 8); b++)
                    specbools[i, b] = BitRegion[b];
            }

            // Fill Language arrays
            byte[] langdata = new Byte[0x280];
            Array.Copy(sav, dexoffset + 0x400, langdata, 0, 0x280);
            BitArray LangRegion = new BitArray(langdata);
            for (int b = 0; b < (721); b++) // 721 Species
                for (int i = 0; i < 7; i++) // 7 Languages
                    langbools[i, b] = LangRegion[7 * b + i];

            // Fill Foreign array
            {
                byte[] foreigndata = new Byte[0x52];
                Array.Copy(sav, dexoffset + 0x688, foreigndata, 0, 0x52);
                BitArray ForeignRegion = new BitArray(foreigndata);
                for (int b = 0; b < (0x52 * 8); b++)
                    foreignbools[b] = ForeignRegion[b];
            }
        }
        private void changeCBSpecies(object sender, EventArgs e)
        {
            if (!editing)
            {
                editing = true;
                int index = (int)CB_Species.SelectedValue;
                LB_Species.SelectedIndex = index - 1; // Since we don't allow index0 in combobox, everything is shifted by 1
                LB_Species.TopIndex = (int)(LB_Species.SelectedIndex);
                loadchks();
                editing = false;
            }
        }
        private void changeLBSpecies(object sender, EventArgs e)
        {
            if (!editing)
            {
                editing = true;
                try
                {
                    int index = LB_Species.SelectedIndex + 1;
                    CB_Species.SelectedValue = index;
                }
                catch { };
                loadchks();
                editing = false;
            }
        }
        private void loadchks()
        {
            // Load Bools for the data
            int pk = 0;
            try
            { pk = (int)((PKHeX.cbItem)(CB_Species.SelectedItem)).Value; }
            catch { pk = (int)LB_Species.SelectedIndex + 1; }

            CheckBox[] CP = new CheckBox[] {
                CHK_P1,CHK_P2,CHK_P3,CHK_P4,CHK_P5,CHK_P6,CHK_P7,CHK_P8,CHK_P9,CHK_P10,
            };
            CheckBox[] CL = new CheckBox[] {
                CHK_L1,CHK_L2,CHK_L3,CHK_L4,CHK_L5,CHK_L6,CHK_L7,
            };
            // Load Partitions
            for (int i = 0; i < 10; i++)
                CP[i].Checked = specbools[i, pk-1];
            for (int i = 0; i < 7; i++)
                CL[i].Checked = langbools[i, pk-1];

            if (pk < 650) { CHK_F1.Enabled = true; CHK_F1.Checked = foreignbools[pk - 1]; }
            else { CHK_F1.Enabled = CHK_F1.Checked = false; }

            if (pk > 721)
            {
                //CHK_P1.Checked = CHK_P1.Enabled = false;
                //CHK_P10.Checked = CHK_P10.Enabled = false;
                //CHK_P6.Enabled = CHK_P7.Enabled = CHK_P8.Enabled = CHK_P9.Enabled = false;

                for (int i = 0; i < 10; i++)
                    CP[i].Enabled = true;

                for (int i = 0; i < 7; i++)
                    CL[i].Checked = CL[i].Enabled = false;
            }
            else
            {
                CHK_P1.Enabled = true;
                CHK_P10.Enabled = true;

                int index = LB_Species.SelectedIndex + 1;
                DataTable spectable = PKX.SpeciesTable();
                int gt = (int)spectable.Rows[index][8];

                CHK_P2.Enabled = CHK_P4.Enabled = CHK_P6.Enabled = CHK_P8.Enabled = (((gt > 255) && (gt != 257)) || gt < 256);
                CHK_P3.Enabled = CHK_P5.Enabled = CHK_P7.Enabled = CHK_P9.Enabled = (gt != 256) && (gt != 258);

                for (int i = 0; i < 7; i++)
                    CL[i].Enabled = true;
            }

            // Load Encountered Count
            editing = true;
            maskedTextBox1.Text = BitConverter.ToUInt16(sav, dexoffset + 0x6E8 + (pk - 1) * 2).ToString();
            editing = false;
        }
        private void removedropCB(object sender, KeyEventArgs e)
        {
            ((ComboBox)sender).DroppedDown = false;
        }
        private void changeLanguageBool(object sender, EventArgs e)
        {
            int species = LB_Species.SelectedIndex + 1;
            langbools[0, (species - 1)] = CHK_L1.Checked;
            langbools[1, (species - 1)] = CHK_L2.Checked;
            langbools[2, (species - 1)] = CHK_L3.Checked;
            langbools[3, (species - 1)] = CHK_L4.Checked;
            langbools[4, (species - 1)] = CHK_L5.Checked;
            langbools[5, (species - 1)] = CHK_L6.Checked;
            langbools[6, (species - 1)] = CHK_L7.Checked;
        }
        private void changePartitionBool(object sender, EventArgs e)
        {
            int species = LB_Species.SelectedIndex + 1;
            specbools[0, (species - 1)] = CHK_P1.Checked;
            specbools[1, (species - 1)] = CHK_P2.Checked;
            specbools[2, (species - 1)] = CHK_P3.Checked;
            specbools[3, (species - 1)] = CHK_P4.Checked;
            specbools[4, (species - 1)] = CHK_P5.Checked;
            specbools[5, (species - 1)] = CHK_P6.Checked;
            specbools[6, (species - 1)] = CHK_P7.Checked;
            specbools[7, (species - 1)] = CHK_P8.Checked;
            specbools[8, (species - 1)] = CHK_P9.Checked;
            specbools[9, (species - 1)] = CHK_P10.Checked;
            if (CHK_F1.Enabled) // species < 650 // (1-649)
                foreignbools[species - 1] = CHK_F1.Checked;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            saveChanges();

            // Return back to the parent savefile
            Array.Copy(sav, m_parent.savefile, sav.Length);
            this.Close();
        }
        private void saveChanges()
        {
            // Save back the Species Bools 
            // Return to Byte Array        
            for (int p = 0; p < 10; p++)
            {
                byte[] sdata = new byte[0x60];

                for (int i = 0; i < 0x60 * 8; i++)
                    if (specbools[p, i])
                        sdata[i / 8] |= (byte)(1 << i % 8);

                Array.Copy(sdata, 0, sav, sv + dexoffset + 0x8 + 0x60 * p, 0x60);
            }

            // Build new bool array for the Languages
            {
                bool[] languagedata = new bool[0x27C * 8];
                for (int i = 0; i < 722; i++)
                    for (int l = 0; l < 7; l++)
                        languagedata[i * 7 + l] = langbools[l, i];

                // Return to Byte Array
                byte[] ldata = new byte[languagedata.Length / 8];

                for (int i = 0; i < languagedata.Length; i++)
                    if (languagedata[i])
                        ldata[i / 8] |= (byte)(1 << i % 8);

                Array.Copy(ldata, 0, sav, sv + dexoffset + 0x400, 0x27C);
            }

            // Return Foreign Array
            {
                byte[] foreigndata = new byte[0x52];
                for (int i = 0; i < 0x52 * 8; i++)
                    if (foreignbools[i])
                        foreigndata[i / 8] |= (byte)(1 << i % 8);
                Array.Copy(foreigndata, 0, sav, sv + dexoffset + 0x688, 0x52);
            }

            // Store Spinda Spot
            try
            {
                uint PID = Util.getHEXval(TB_Spinda);
                Array.Copy(BitConverter.GetBytes(PID), 0, sav, dexoffset + 0x680, 4);
            }
            catch { };
        }

        private void B_GiveAll_Click(object sender, EventArgs e)
        {
            if (LB_Species.SelectedIndex > 0x2D0) return;
            if (CHK_L1.Enabled)
            {
                CHK_L1.Checked =
                CHK_L2.Checked =
                CHK_L3.Checked =
                CHK_L4.Checked =
                CHK_L5.Checked =
                CHK_L6.Checked =
                CHK_L7.Checked = !(ModifierKeys == Keys.Control);
            }
            if (CHK_P1.Enabled)
            {
                CHK_P1.Checked =
                CHK_P10.Checked = !(ModifierKeys == Keys.Control);
            }
            if (CHK_F1.Enabled)
            {
                CHK_F1.Checked = !(ModifierKeys == Keys.Control);
            }
            int index = LB_Species.SelectedIndex+1;
            DataTable spectable = PKX.SpeciesTable();
            int gt = (int)spectable.Rows[index][8];

            CHK_P2.Checked = CHK_P4.Checked = CHK_P6.Checked = CHK_P8.Checked = (((gt > 255) && (gt != 257)) || gt < 256) && !(ModifierKeys == Keys.Control);
            CHK_P3.Checked = CHK_P5.Checked = CHK_P7.Checked = CHK_P9.Checked = (gt != 256) && (gt != 258) && !(ModifierKeys == Keys.Control);

            changePartitionBool(null, null);
            changeLanguageBool(null, null);
            LB_Species.SelectedIndex++;
        }
        private void B_FillDex_Click(object sender, EventArgs e)
        {
            saveChanges();
            int index = LB_Species.SelectedIndex;
            // Copy Full Dex Byte Array
            byte[] fulldex = Properties.Resources.fulldex_XY;
            if (ModifierKeys != Keys.Control)
            {
                Array.Copy(fulldex, 0x008, sav, dexoffset + 0x8, 0x638);
            }
            else
            {
                Array.Copy(fulldex, 0x008, sav, dexoffset + 0x8, 0x1E0);   // Copy Partitions 1-5
                Array.Copy(fulldex, 0x368, sav, dexoffset + 0x368, 0x2D8); // Copy A & language
            }
            // Skip the unknown sections then
            Array.Copy(fulldex, 0x64C, sav, dexoffset + 0x64C, 0x054);

            editing = true;
            Setup();
            editing = false;
            LB_Species.SelectedIndex = index; // restore selection
        }

        private void changeEncounteredCount(object sender, EventArgs e)
        {
            if (!editing)
                Array.Copy(BitConverter.GetBytes(Util.ToUInt32(maskedTextBox1)), 0, sav, dexoffset + 0x6E8 + (LB_Species.SelectedIndex - 1) * 2, 2);
        }
    }
}
