using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateNewTab(); // Khi mở chương trình sẽ có sẵn 1 tab mới
        }

        // 🧱 Lấy RichTextBox hiện tại trong tab đang chọn
        private RichTextBox GetCurrentRichTextBox()
        {
            if (tabControlMain.SelectedTab != null && tabControlMain.SelectedTab.Controls.Count > 0)
                return tabControlMain.SelectedTab.Controls[0] as RichTextBox;
            return null;
        }

        // 🧱 Tạo tab mới
        private void CreateNewTab(string title = "Untitled")
        {
            TabPage newTab = new TabPage(title);
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            rtb.Font = new Font("Consolas", 11);
            newTab.Controls.Add(rtb);
            tabControlMain.TabPages.Add(newTab);
            tabControlMain.SelectedTab = newTab;
        }

        // 🆕 File → New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        // 📂 File → Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogMain.FileName;
                string fileText = File.ReadAllText(filePath, Encoding.UTF8);
                CreateNewTab(Path.GetFileName(filePath));

                RichTextBox rtb = GetCurrentRichTextBox();
                rtb.Text = fileText;
                rtb.Tag = filePath; // Lưu đường dẫn để Save nhanh
            }
        }

        // 💾 File → Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            string filePath = rtb.Tag as string;

            if (string.IsNullOrEmpty(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e); // Nếu chưa lưu, mở Save As
                return;
            }

            File.WriteAllText(filePath, rtb.Text, Encoding.UTF8);
            tabControlMain.SelectedTab.Text = Path.GetFileName(filePath);
        }

        // 💾 File → Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialogMain.FileName;
                File.WriteAllText(filePath, rtb.Text, Encoding.UTF8);

                rtb.Tag = filePath;
                tabControlMain.SelectedTab.Text = Path.GetFileName(filePath);
            }
        }

        // ❌ File → Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        // --- SỬ DỤNG GetCurrentRichTextBox() THAY CHO richTextBox1 ---

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return; // Kiểm tra nếu không có tab nào đang mở

            rtb.Focus();
            // Kiểm tra xem có văn bản nào được chọn không
            if (rtb.SelectionLength > 0)
            {
                rtb.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            // Kiểm tra xem có văn bản nào được chọn không
            if (rtb.SelectionLength > 0)
            {
                rtb.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            // Kiểm tra xem clipboard có chứa văn bản không
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                rtb.Paste();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            rtb.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            rtb.SelectAll();
        }
    }
}
