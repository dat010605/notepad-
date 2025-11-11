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

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            // Giới hạn phóng to để không bị quá lớn
            if (rtb.ZoomFactor < 5.0f)
            {
                rtb.ZoomFactor += 0.2f; // Tăng kích thước zoom lên 0.2
            }
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (rtb.CanUndo)
            {
                rtb.Undo();
            }
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (rtb.CanRedo)
            {
                rtb.Redo();
            }
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            // Giới hạn thu nhỏ để không bị quá bé
            if (rtb.ZoomFactor > 0.5f)
            {
                rtb.ZoomFactor -= 0.2f; // Giảm kích thước zoom đi 0.2
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("zalo 0393175190 ");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        { }
      
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem TabControl có đang chứa bất kỳ Tab nào không
            // (Giả sử TabControl của bạn có tên là tabControl1)
            if (tabControlMain.TabPages.Count > 0)
            {
                // Lấy TabPage đang được chọn (Tab người dùng đang xem)
                TabPage tabToRemove = tabControlMain.SelectedTab;

                // Xóa TabPage đó khỏi TabControl
                tabControlMain.TabPages.Remove(tabToRemove);

                // Tùy chọn: Xử lý nếu đó là Tab cuối cùng
                if (tabControlMain.TabPages.Count == 0)
                {
                    // Ví dụ: Tạo lại một Tab mới trống (thường là hành vi mong muốn cho Notepad)
                    TabPage newTabPage = new TabPage("Untitled");
                    RichTextBox newRichTextBox = new RichTextBox { Dock = DockStyle.Fill };
                    newTabPage.Controls.Add(newRichTextBox);
                    tabControlMain.TabPages.Add(newTabPage);
                    tabControlMain.SelectedTab = newTabPage;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // 1. Đếm số lượng tab hiện có để đặt tên cho tab mới
            int newTabIndex = tabControlMain.TabPages.Count + 1;

            // 2. Tạo một TabPage mới
            TabPage newTabPage = new TabPage("Untitled " + newTabIndex);

            // 3. Tạo RichTextBox mới
            RichTextBox newRichTextBox = new RichTextBox();

            // 4. Cấu hình RichTextBox để lấp đầy toàn bộ TabPage
            newRichTextBox.Dock = DockStyle.Fill;
            newRichTextBox.Name = "RichTB_" + newTabIndex; // Đặt tên riêng để dễ quản lý sau này

            // 5. Thêm RichTextBox vào TabPage
            newTabPage.Controls.Add(newRichTextBox);

            // 6. Thêm TabPage mới vào TabControl
            tabControlMain.TabPages.Add(newTabPage);

            // 7. Chuyển ngay sang Tab mới tạo để người dùng bắt đầu làm việc
            tabControlMain.SelectedTab = newTabPage;
        }
    }
}
