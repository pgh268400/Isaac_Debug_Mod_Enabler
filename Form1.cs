using IniParser;
using IniParser.Model;
using System.Diagnostics;

namespace IsaacDebugConsoleEnabler
{
    public partial class Form1 : Form
    {
        //싱글톤 패턴
        DebugConsoleManager dbg = new DebugConsoleManager();

        //저장할 ini 파일 경로
        string settings_file_path = "./settings.ini";

        public Form1()
        {
            InitializeComponent();
        }

        private void restart_game(object sender, EventArgs e)
        {
            bool isSuccess = dbg.restart_process("isaac-ng");
            if (!isSuccess) MessageBox.Show("아이작이 실행중이지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void run_game(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("isaac-ng");
            if (processes.Length > 0)
            {
                MessageBox.Show("아이작이 이미 실행중입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSuccess = dbg.run_process(textbox_game_path.Text);
            if (!isSuccess) MessageBox.Show("아이작이 실행에 실패했습니다 게임 경로를 다시 확인해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            //파일이 존재하면 settings.ini 에서 아이작 경로를 받아오고, 아니면 자동 인식시킨다. (저장은 Form closing 에서 처리함)
            if (File.Exists(settings_file_path))
            {
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(settings_file_path);

                textbox_game_path.Text = data["Settings"]["GamePath"];
            }
            else
            {
                //전체 드라이브에서 아이작 게임 파일 경로를 찾아서 텍스트박스에 넣어준다.
                string result = await dbg.search_file_all_dir_async("isaac-ng.exe");
                textbox_game_path.Text = result;
            }

            //모드 / 디버그 콘솔 활성화 여부를 체크박스에 넣어준다.
            checkbox_mod.Checked = dbg.get_mod_status();
            checkbox_console.Checked = dbg.get_debug_console();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //파일 다이얼로그 열기
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "exe 파일|*.exe";
            ofd.Title = "아이작 실행파일을 선택해주세요.";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //ofd.FileName 이 isaac-ng.exe 인 경우만 
                if (ofd.FileName.ToLower() == "isaac-ng.exe")
                    textbox_game_path.Text = ofd.FileName;
                else
                    MessageBox.Show("아이작 실행파일을 선택해주세요. [Need : isaac-ng.exe]", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void checkbox_mod_Click(object sender, EventArgs e)
        {
            if (checkbox_mod.Checked)
            {
                dbg.set_mod_status(true);
                MessageBox.Show("모드가 활성화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                dbg.set_mod_status(false);
                MessageBox.Show("모드가 비활성화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void checkbox_console_Click(object sender, EventArgs e)
        {
            if (checkbox_console.Checked)
            {
                dbg.set_debug_console(true);
                MessageBox.Show("콘솔이 활성화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dbg.set_debug_console(false);
                MessageBox.Show("콘솔이 비활성화되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //파일이 존재하지 않으면 settings.ini 를 만들고 아이작 경로를 저장한다
            if (!File.Exists(settings_file_path))
            {
                //settings.ini 파일 생성
                File.Create(settings_file_path).Close();
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(settings_file_path);

                data["Settings"]["GamePath"] = textbox_game_path.Text;

                parser.WriteFile(settings_file_path, data);
            }
        }
    }
}