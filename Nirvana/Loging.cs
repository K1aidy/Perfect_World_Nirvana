using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Nirvana
{
    public class Loging
    {
        static String token = String.Empty;
        static String userId_1 = String.Empty;
        static String userId_2 = String.Empty;
        public static String Downloader;
        public static String UserId_1;
        public static String UserId_2;

        public static async Task<String> GET(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.Accept = @"*/*";
            request.UserAgent = @"Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 " + Downloader + @" Safari/537.36";
            request.AllowAutoRedirect = false;
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            String answer = String.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    answer = reader.ReadToEnd();
                }
            }
            //получаем коллекцию хедеров
            WebHeaderCollection headers = response.Headers;

            response.Close();
            //получаем куки
            String[] result = headers.GetValues("Set-Cookie");
            //в цикле ищем необходимые куки
            foreach (String s in result)
                if (s.Contains("Mpop")) return s;

            return String.Empty;
        }

        public static async Task<String> POST(String URL, String data)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.Accept = @"*/*";
                request.UserAgent = @"Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 " + Downloader + @" Safari/537.36";
                request.ContentType = @"application/x-www-form-urlencoded";
                //формируем байтовый массив
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                //указываем объем передаваемых данных
                request.ContentLength = byteArray.Length;
                //записываем данные в поток запроса
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                //получаем ответ на запрос
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                //получаем текстовое представление ответа
                String answer = String.Empty;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        answer += reader.ReadToEnd();
                    }
                }
                response.Close();
                return answer;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    MessageBox.Show(((HttpWebResponse)e.Response).StatusCode.ToString());
                }
                return String.Empty;
            }
        }

        public static async void AuthAsync(Account user, MainWindow headWindow)
        {
            String temp = String.Empty;
            String answer_1 = String.Empty;
            #region АВТОРИЗАЦИЯ НА ПОЧТЕ
            if (user.TsaToken != String.Empty)
            {
                //делаем запрос по сохраненному токену
                temp = String.Format("client_id=gamecenter.mail.ru&grant_type=password&username={0}&password={1}&tsa_token={2}", user.Mail, user.Password, user.TsaToken);
                Regex regex_1_1 = new Regex("\"tsa_token\":\".{32}\"");
                Regex regex_1_2 = new Regex("\"access_token\":\".{48}\"");
                answer_1 = await POST("https://o2.mail.ru//token", temp);
                user.TsaToken = regex_1_1.Match(answer_1).Value;
                user.TsaToken = user.TsaToken.Replace("\"tsa_token\":\"", "");
                user.TsaToken = user.TsaToken.Replace("\"", "");
                SaveAccounts();
                answer_1 = regex_1_2.Match(answer_1).Value;
                answer_1 = answer_1.Replace("\"access_token\":\"", "");
                answer_1 = answer_1.Replace("\"", "");
            }
            else
            {
                temp = @"client_id=gamecenter.mail.ru&grant_type=password&username=" + user.Mail + @"&password=" + user.Password;
                answer_1 = await POST("https://o2.mail.ru//token", temp);
                if (answer_1.Contains("second step code check required to authenticate user") || answer_1.Contains("sms send timeout"))
                {
                    Regex regex_1 = new Regex("\"tsa_token\":\".{32}\"");
                    do
                    {
                        answer_1 = regex_1.Match(answer_1).Value;
                        answer_1 = answer_1.Replace("\"tsa_token\":\"", "");
                        answer_1 = answer_1.Replace("\"", "");
                        //сохраняем полученный tsa токен для будущего использования
                        user.TsaToken = answer_1;
                        SaveAccounts();
                        //запрашиваем СМС код
                        headWindow.ShowQuestions();
                        //отправляем запрос с кодом из смс
                        temp = String.Format("client_id=gamecenter.mail.ru&grant_type=password&username={0}&tsa_token={1}&auth_code={2}&permanent=1", user.Mail, user.TsaToken, DataSms.smsCode);
                        answer_1 = await POST("https://o2.mail.ru//token", temp);
                        DataSms.smsCode = String.Empty;
                    }
                    while (answer_1.Contains("invalid auth_code was submitted"));
                }
                user.TsaToken = new Regex("\"tsa_token\":\".{32}").Match(answer_1).Value.Replace("\"tsa_token\":\"", "");
                SaveAccounts();
                Regex regex_1_1 = new Regex(".access_token...\\S{48}.}");
                answer_1 = regex_1_1.Match(answer_1).Value;
                answer_1 = answer_1.Replace("\"access_token\":\"", "");
                answer_1 = answer_1.Replace("\"}", "");
            }



            temp = @"client_id=gamecenter.mail.ru&access_token=" + answer_1;
            String answer_2 = await POST(@"https://o2.mail.ru/userinfo", temp);

            temp = String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><MrPage2 SessionKey=\"{0}\" Page=\"http://dl.mail.ru/robots.txt\"/>", answer_1);
            Regex regex_3 = new Regex("Location=\".*\" ErrorCode");
            String answer_3 = regex_3.Match(await POST(@"https://authdl.mail.ru/ec.php?hint=MrPage2", temp)).Value;
            answer_3 = answer_3.Replace("Location=\"", "");
            answer_3 = answer_3.Replace("\" ErrorCode", "");
            answer_3 = answer_3.Replace("&amp;", "&");

            String answer_4 = await GET(answer_3);
            Regex regex_4 = new Regex("Mpop=.*;");
            answer_4 = regex_4.Match(answer_4).Value;
            answer_4 = answer_4.Replace("Mpop=", "");
            answer_4 = answer_4.Replace(";", "");

            temp = String.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><Auth UserId=""" + UserId_1 + @""" UserId2=""" + UserId_2 + @""" Soft=""1"" Cookie=""{0}"" AppId="""" ChannelId=""27""/>", answer_4);
            String answer_5 = await POST(@"https://authdl.mail.ru/ec.php?hint=Auth", temp);
            #endregion

            #region АВТОРИЗАЦИЯ В ИГРЕ
            temp = String.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><AutoLogin ProjectId=""61"" SubProjectId=""0"" ShardId=""0"" UserId=""01234567890123456789"" UserId2=""01234567890123456789"" Mpop=""{0}"" FirstLink=""_1lp=1&amp;_1ld=300&amp;_1lnh=1"" RegionData=""""/>", answer_4);
            String answer_6 = await POST(@"https://authdl.mail.ru/sz.php?hint=AutoLogin", temp);
            temp = String.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><PersList ProjectId=""61"" SubProjectId=""0"" ShardId=""0"" UserId=""01234567890123456789"" UserId2=""01234567890123456789"" Mpop=""{0}""/>", answer_4);
            String answer_7 = await POST(@"https://authdl.mail.ru/sz.php?hint=AutoLogin", temp);

            Regex regex_uid_2 = new Regex("AutoLogin PersId=\"\\d*\"");
            Regex regex_uid_1 = new Regex("Pers Id=\"\\d*\"");
            Regex regex_token = new Regex("Key=\"\\w*\"");

            userId_1 = regex_uid_1.Match(answer_7).Value;
            userId_1 = userId_1.Replace("Pers Id=\"", "");
            userId_1 = userId_1.Replace("\"", "");

            userId_2 = regex_uid_2.Match(answer_6).Value;
            userId_2 = userId_2.Replace("AutoLogin PersId=\"", "");
            userId_2 = userId_2.Replace("\"", "");

            token = regex_token.Match(answer_6).Value;
            token = token.Replace("Key=\"", "");
            token = token.Replace("\"", "");
            #endregion

            #region ЗАПУСК КЛИЕНТА

            Process process = new Process();
            process.StartInfo.WorkingDirectory = @"C:\GamesMailRu\Perfect World\element\";
            process.StartInfo.FileName = "elementclient.exe";
            process.StartInfo.Arguments = String.Format("startbypatcher user:{0} _user:{1} token2:{2}", userId_1, userId_2, token);
            process.StartInfo.Verb = "runas";
            process.Start();

            #endregion
        }

        /// <summary>
        /// Сохранение коллекции аккаунтов в XML
        /// </summary>
        private  static void SaveAccounts()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Account>));
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\password.xml", FileMode.Truncate))
            {
                formatter.Serialize(fs, Collection.accounts);
                fs.Close();
            }
        }

    }
}
