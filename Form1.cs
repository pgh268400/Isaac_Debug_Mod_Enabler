using IniParser;
using IniParser.Model;
using System.Diagnostics;

namespace IsaacDebugConsoleEnabler
{
    public partial class Form1 : Form
    {
        //�̱��� ����
        DebugConsoleManager dbg = new DebugConsoleManager();

        //������ ini ���� ���
        string settings_file_path = "./settings.ini";

        public Form1()
        {
            InitializeComponent();
        }

        private void restart_game(object sender, EventArgs e)
        {
            bool isSuccess = dbg.restart_process("isaac-ng");
            if (!isSuccess) MessageBox.Show("�������� ���������� �ʽ��ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void run_game(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("isaac-ng");
            if (processes.Length > 0)
            {
                MessageBox.Show("�������� �̹� �������Դϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSuccess = dbg.run_process(textbox_game_path.Text);
            if (!isSuccess) MessageBox.Show("�������� ���࿡ �����߽��ϴ� ���� ��θ� �ٽ� Ȯ�����ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            //������ �����ϸ� settings.ini ���� ������ ��θ� �޾ƿ���, �ƴϸ� �ڵ� �νĽ�Ų��. (������ Form closing ���� ó����)
            if (File.Exists(settings_file_path))
            {
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(settings_file_path);

                textbox_game_path.Text = data["Settings"]["GamePath"];
            }
            else
            {
                //��ü ����̺꿡�� ������ ���� ���� ��θ� ã�Ƽ� �ؽ�Ʈ�ڽ��� �־��ش�.
                string result = await dbg.search_file_all_dir_async("isaac-ng.exe");
                textbox_game_path.Text = result;
            }

            //��� / ����� �ܼ� Ȱ��ȭ ���θ� üũ�ڽ��� �־��ش�.
            checkbox_mod.Checked = dbg.get_mod_status();
            checkbox_console.Checked = dbg.get_debug_console();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //���� ���̾�α� ����
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "exe ����|*.exe";
            ofd.Title = "������ ���������� �������ּ���.";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //ofd.FileName �� isaac-ng.exe �� ��츸 
                if (ofd.FileName.ToLower() == "isaac-ng.exe")
                    textbox_game_path.Text = ofd.FileName;
                else
                    MessageBox.Show("������ ���������� �������ּ���. [Need : isaac-ng.exe]", "���", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void checkbox_mod_Click(object sender, EventArgs e)
        {
            if (checkbox_mod.Checked)
            {
                dbg.set_mod_status(true);
                MessageBox.Show("��尡 Ȱ��ȭ�Ǿ����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                dbg.set_mod_status(false);
                MessageBox.Show("��尡 ��Ȱ��ȭ�Ǿ����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void checkbox_console_Click(object sender, EventArgs e)
        {
            if (checkbox_console.Checked)
            {
                dbg.set_debug_console(true);
                MessageBox.Show("�ܼ��� Ȱ��ȭ�Ǿ����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dbg.set_debug_console(false);
                MessageBox.Show("�ܼ��� ��Ȱ��ȭ�Ǿ����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //������ �������� ������ settings.ini �� ����� ������ ��θ� �����Ѵ�
            if (!File.Exists(settings_file_path))
            {
                //settings.ini ���� ����
                File.Create(settings_file_path).Close();
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(settings_file_path);

                data["Settings"]["GamePath"] = textbox_game_path.Text;

                parser.WriteFile(settings_file_path, data);
            }
        }
    }
}