using IniParser;
using IniParser.Model;
using System.Diagnostics;

namespace IsaacDebugConsoleEnabler
{
    /// <summary>
    /// 아이작 디버그 콘솔을 관리하기 위한 클래스 (==객체 설계도, Singletone)
    class DebugConsoleManager
    {

        //객체 변수(필드, 또는 멤버 변수라고도 함)
        string options_file_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Binding of Isaac Repentance\options.ini";

        // 생성자 정의 (기본 생성자 오버로딩)
        public DebugConsoleManager()
        {
        }
        public DebugConsoleManager(string file_path)
        {
            options_file_path = file_path;
        }

        //Options.ini 값 조정 함수
        public void set_ini_options(string key, string value)
        {
            if (File.Exists(options_file_path))
            {
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(options_file_path);

                data["Options"][key] = value;

                parser.WriteFile(options_file_path, data);
            }
        }

        //Options.ini 값 가져오기 함수

        public string? get_ini_options(string key)
        {
            if (File.Exists(options_file_path))
            {
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(options_file_path);

                return data["Options"][key];
            }
            else
            {
                return null;
            }
        }

        // 디버그 콘솔 조작 함수 (실제로는 set_options를 호출하며, 사용자 입장에선 이 함수를 사용하면 됨. (인터페이스용))
        public void set_debug_console(bool status)
        {
            //bool을 str로 변환
            string str = status ? "1" : "0";
            set_ini_options("EnableDebugConsole", str);
        }

        //디버그 콘솔 상태 확인
        public bool get_debug_console()
        {
            //string -> bool 변환
            return (get_ini_options("EnableDebugConsole") == "1");
        }

        public void set_mod_status(bool status)
        {
            //bool을 str로 변환
            string str = status ? "1" : "0";
            set_ini_options("EnableMods", str);
        }

        public bool get_mod_status()
        {
            //string -> bool 변환
            return (get_ini_options("EnableMods") == "1");
        }


        //프로세스 재시작 함수
        public bool restart_process(string process_name)
        {
            //ex) process_name : "notepad" (exe 미포함)
            Process[] processes = Process.GetProcessesByName(process_name);

            if (processes.Length > 0)
            {
                string process_path = processes[0].MainModule.FileName; ; //실행중인 프로세스 경로
                foreach (Process process in processes)
                {
                    process.Kill();
                    process.WaitForExit();
                }

                Process.Start(process_path);
                return true; //재시작 성공
            }
            else
            {
                return false; //재시작 실패
            }
        }

        public bool run_process(string process_path)
        {
            //경로로 제공한 프로세스 파일이 존재하는 경우
            if (File.Exists(process_path))
            {
                Process.Start(process_path); //프로세스 실행
                return true;  // 성공 여부 반환
            }
            else
            {
                return false;
            }
        }


        //root 경로에서 재귀적으로 모든 파일을 탐색하고 "file_name" 으로 시작하는 파일의 절대 경로를 취득
        //취소 토큰에서 취소가 감지되면 즉시 중단
        public string search_file_all_dir(string file_name, string root = @"C:\", CancellationToken token = default)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(root))
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dir, file_name);

                        if (files.Length > 0)
                        {
                            return files[0];
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (token.IsCancellationRequested)
                    {
                        return null;
                    }

                    string found_file = search_file_all_dir(file_name, dir, token);

                    if (found_file != null)
                    {
                        return found_file;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }

            return null;
        }

        //파일 탐색 함수를 비동기 & 병렬로 돌려서
        //전체 드라이브에서 파일을 빠르게 탐색하여 "file_name" 의 절대 경로를 획득할 수 있도록 함.
        public async Task<string> search_file_all_dir_async(string filename)
        {
            string[] drives = Environment.GetLogicalDrives();
            var tasks = new List<Task<string?>>();
            CancellationTokenSource cts = new CancellationTokenSource();

            foreach (string drive in drives)
            {
                Console.WriteLine($"Searching {drive}...");

                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        string result = search_file_all_dir(filename, drive, cts.Token);
                        Console.WriteLine($"Completed search on {drive}");
                        return result;
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }));
            }

            while (tasks.Count > 0)
            {
                Task<string> finishedTask = await Task.WhenAny(tasks);

                if (!string.IsNullOrEmpty(finishedTask.Result))
                {
                    cts.Cancel();
                    return finishedTask.Result;
                }

                tasks.Remove(finishedTask);
            }

            return null;
        }
    }
}
