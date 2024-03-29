﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PKHeX
{
    public partial class SAV_Wondercard : Form
    {
        public SAV_Wondercard(Form1 frm1)
        {
            InitializeComponent();
            m_parent = frm1;
            Array.Copy(m_parent.savefile, sav, 0x100000);
            savindex = m_parent.savindex;
            if (m_parent.savegame_oras) wcoffset = 0x22100;
            populateWClist();
            populateReceived();

            LB_WCs.SelectedIndex = 0;

            if (LB_Received.Items.Count > 0)
                LB_Received.SelectedIndex = 0;            
        }
        Form1 m_parent;
        public byte[] sav = new Byte[0x100000];
        public byte[] wondercard_data = new Byte[0x108];
        public int savindex;
        public bool editing = false;
        private static uint ToUInt32(String value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            return UInt32.Parse(value);
        }
        private int wcoffset = 0x21100;

        // Repopulation Functions
        private void populateWClist()
        {
            LB_WCs.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                int offset = wcoffset + savindex * 0x7F000 + i * 0x108;
                int cardID = BitConverter.ToUInt16(sav, offset);
                if (cardID == 0)
                    LB_WCs.Items.Add((i + 1).ToString("00") + " - Empty");
                else
                    LB_WCs.Items.Add((i + 1).ToString("00") + " - " + cardID.ToString("0000"));
            }
        }
        private void loadwcdata()
        {
            // Load up the data according to the wiki!
            int cardID = BitConverter.ToUInt16(wondercard_data, 0);
            if (cardID == 0)
            {
                RTB.Text = "Empty Slot. No data!";
                return;
            }
            string cardname = Encoding.Unicode.GetString(wondercard_data, 0x2, 0x48);
            int cardtype = wondercard_data[0x51];
            RTB.Clear();
            RTB.AppendText("Card #: " + cardID.ToString("0000") + "\r\n" + cardname + "\r\n");

            if (cardtype == 1) // Item
            {
                int item = BitConverter.ToUInt16(wondercard_data, 0x68);
                int qty = BitConverter.ToUInt16(wondercard_data, 0x70);

                RTB.AppendText("\r\nItem: " + Form1.itemlist[item] + "\r\n" + "Quantity: " + qty.ToString());
            }
            else if (cardtype == 0) // PKM
            {
                int species = BitConverter.ToUInt16(wondercard_data, 0x82);
                int helditem = BitConverter.ToUInt16(wondercard_data, 0x78);
                int move1 = BitConverter.ToUInt16(wondercard_data, 0x7A);
                int move2 = BitConverter.ToUInt16(wondercard_data, 0x7C);
                int move3 = BitConverter.ToUInt16(wondercard_data, 0x7E);
                int move4 = BitConverter.ToUInt16(wondercard_data, 0x80);
                int TID = BitConverter.ToUInt16(wondercard_data, 0x68);
                int SID = BitConverter.ToUInt16(wondercard_data, 0x6A);

                string OTname = Util.TrimFromZero(Encoding.Unicode.GetString(wondercard_data, 0xB6, 22));
                RTB.Text +=
                    "\r\nSpecies: " + Form1.specieslist[species] + "\r\n"
                    + "Item: " + Form1.itemlist[helditem] + "\r\n"
                    + "Move 1: " + Form1.movelist[move1] + "\r\n"
                    + "Move 2: " + Form1.movelist[move2] + "\r\n"
                    + "Move 3: " + Form1.movelist[move3] + "\r\n"
                    + "Move 4: " + Form1.movelist[move4] + "\r\n"
                    + "OT: " + OTname + "\r\n"
                    + "ID: " + TID.ToString() + "/" + SID.ToString();
            }
            else
                RTB.Text = "Unsupported Wondercard Type!";
        }
        private void populateReceived()
        {
            int offset = wcoffset - 0x100 + savindex * 0x7F000;
            LB_Received.Items.Clear();
            for (int i = 1; i < 2048; i++)
                if ((((sav[offset + i / 8]) >> (i % 8)) & 0x1) == 1)
                    LB_Received.Items.Add(i.ToString("0000"));
        }

        // Wondercard IO (.wc6<->window)
        private void B_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog importwc6 = new OpenFileDialog();
            importwc6.Filter = "Wondercard|*.wc6";
            if (importwc6.ShowDialog() == DialogResult.OK)
            {
                string path = importwc6.FileName;
                byte[] newwc6 = File.ReadAllBytes(path);
                if (newwc6.Length > 0x108)
                {
                    MessageBox.Show("Not a valid wondercard length.", "Error");
                    return;
                }
                Array.Resize(ref newwc6, 0x108);
                Array.Copy(newwc6, wondercard_data, 0x108);
                loadwcdata();
            }
        }
        private void B_Output_Click(object sender, EventArgs e)
        {
            SaveFileDialog outputwc6 = new SaveFileDialog();
            int cardID = BitConverter.ToUInt16(wondercard_data, 0);
            string cardname = Encoding.Unicode.GetString(wondercard_data, 0x2, 0x48);
            outputwc6.FileName = cardID + " - " + cardname + ".wc6";
            outputwc6.Filter = "Wondercard|*.wc6";
            if (outputwc6.ShowDialog() == DialogResult.OK)
            {
                string path = outputwc6.FileName;

                if (File.Exists(path))
                {
                    // File already exists, save a .bak
                    byte[] backupfile = File.ReadAllBytes(path);
                    File.WriteAllBytes(path + ".bak", backupfile);
                }

                File.WriteAllBytes(path, wondercard_data);
            }
        }

        // Wondercard RW (window<->sav)
        private void B_SAV2WC(object sender, EventArgs e)
        {
            // Load Wondercard from Save File
            int index = LB_WCs.SelectedIndex;
            int offset = wcoffset + savindex * 0x7F000 + index * 0x108;
            Array.Copy(sav, offset, wondercard_data, 0, 0x108);
            loadwcdata();
        }
        private void B_WC2SAV(object sender, EventArgs e)
        {
            // Write Wondercard to Save File
            int index = LB_WCs.SelectedIndex;
            int offset = wcoffset + savindex * 0x7F000 + index * 0x108;
            Array.Copy(wondercard_data, 0, sav, offset, 0x108);
            populateWClist();
            int cardID = BitConverter.ToUInt16(wondercard_data, 0);

            if (cardID > 0)
                if (!LB_Received.Items.Contains(cardID.ToString("0000")))
                    LB_Received.Items.Add(cardID.ToString("0000"));
        }
        private void B_DeleteWC_Click(object sender, EventArgs e)
        {
            int index = LB_WCs.SelectedIndex;
            int offset = wcoffset + savindex * 0x7F000 + index * 0x108;
            byte[] zeros = new Byte[0x108];
            Array.Copy(zeros, 0, sav, offset, 0x108);
            populateWClist();
        }

        // Close Window
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            // Make sure all of the Received Flags are flipped!
            for (int i = 0; i < LB_Received.Items.Count; i++)
            {
                int offset = wcoffset - 0x100 + savindex * 0x7F000;
                string cardID = LB_Received.Items[i].ToString();
                uint cardnum = ToUInt32(cardID);

                sav[offset + cardnum / 8] = (byte)(sav[offset + cardnum / 8] | (1 << ((byte)cardnum % 8)));
            }

            // Make sure there's no space between wondercards
            {
                int offset = wcoffset + savindex * 0x7F000;
                for (int i = 0; i < 24; i++)
                    if (BitConverter.ToUInt16(sav, offset+i*0x108) == 0)
                        for (int j = (i + 1); j < 24 - i; j++) // Shift everything down
                            Array.Copy(sav, offset + (j) * 0x108, sav, offset + (j-1) * 0x108, 0x108);
            }

            Array.Copy(sav, m_parent.savefile, 0x100000);
            m_parent.savedited = true;
            m_parent.setBoxNames();
            m_parent.setPKXBoxes();
            Close();
        }

        private void B_DeleteReceived_Click(object sender, EventArgs e)
        {
            if (LB_Received.SelectedIndex > -1)
            if (LB_Received.Items.Count > 0)
                LB_Received.Items.Remove(LB_Received.Items[LB_Received.SelectedIndex]);
        }
    }
}
