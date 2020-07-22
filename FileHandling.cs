
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordKeeper
{
    public enum CopyValue
    {
        Password = 0,
        UserId
    }

    internal class FileHandling
    {
        static Thread thread = new Thread(CommandLine.RunClear);
        private static string secretFilePath = Constants.CredentialFileName;
        private static bool IsChange = false;

        public static void InsertOperation(Credential credential)
        {
            String[] contents = { credential.Description.Encrypt(), credential.UserName.Encrypt(), credential.Pwd.Encrypt() };
            File.AppendAllLines(FileHandling.secretFilePath, contents);
        }

        public static void ReadCredential(CopyValue copyValue, int credentialSeqNo)
        {
            if (thread.ThreadState == ThreadState.Running || thread.ThreadState==ThreadState.WaitSleepJoin)
            {
                thread.Abort();            
            }

            thread = new Thread(CommandLine.RunClear);
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            for (int i = 1; i <= credentialSeqNo * 3; i++)
            {
                string text = streamReader.ReadLine();
                if (i == 3 * credentialSeqNo - (int)copyValue)
                {
                    text = text.Decrypt();
                    // Console.WriteLine(" username : " + text + " . copied to clipboard");
                    CommandLine.RunCopy(text);
                    break;
                }
            }

            thread.Start();
            streamReader.Close();
        }

        public static List<Credential> ViewAll()
        {
            List<Credential> credentials = new List<Credential>();
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            int num = 1;
            string encryptedstring;
            Credential currCredential = null;
            while (!string.IsNullOrEmpty(encryptedstring = streamReader.ReadLine()))
            {

                if (num % 3 == 1)
                {
                    currCredential = new Credential();
                    currCredential.Description = encryptedstring.Decrypt();
                }
                else if (num % 3 == 2)
                {
                    currCredential.UserName = encryptedstring.Decrypt();
                }
                else
                {
                    currCredential.Pwd = encryptedstring.Decrypt();
                    currCredential.id = num / 3;
                    credentials.Add(currCredential);
                }
                num++;
            }

            streamReader.Close();
            return credentials;
        }

        public static void ModifyOperation(Credential credential)
        {
            List<string> dataFile = File.ReadAllLines(FileHandling.secretFilePath).ToList<string>();
            // credentials.
            for (int index = 0; index < dataFile.Count; index++)
            {
                if (3 * (credential.id - 1) == index)
                {
                    dataFile[index] = credential.Description.Encrypt();
                }
                else if (3 * (credential.id - 1) + 1 == index)
                {
                    dataFile[index] = credential.UserName.Encrypt();
                }
                else if (3 * (credential.id - 1) + 2 == index)
                {
                    dataFile[index] = credential.Pwd.Encrypt();
                }
            }
            string[] contents = dataFile.ToArray();
            File.WriteAllLines(FileHandling.secretFilePath, contents);
        }

        public static void DeleteOperation(Credential credential)
        {
            List<string> credentials = File.ReadAllLines(FileHandling.secretFilePath).ToList<string>();
            credentials.RemoveAt(3 * (credential.id - 1));
            credentials.RemoveAt(3 * (credential.id - 1));
            credentials.RemoveAt(3 * (credential.id - 1));
            string[] contents = credentials.ToArray();
            File.WriteAllLines(FileHandling.secretFilePath, contents);
        }

        #region obsolete
        [Obsolete]
        public static void DeleteOperation()
        {
            Console.WriteLine("Which one to delete ? ");
            FileHandling.ViewAll();
            int num = int.Parse(Console.ReadLine());
            List<string> expr_29 = File.ReadAllLines(FileHandling.secretFilePath).ToList<string>();
            expr_29.RemoveAt(3 * num - 1);
            expr_29.RemoveAt(3 * num - 1);
            expr_29.RemoveAt(3 * num - 1);
            string[] contents = expr_29.ToArray();
            File.WriteAllLines(FileHandling.secretFilePath, contents);
        }

        [Obsolete]
        public static void ModifyOperation()
        {
            // FileHandling.ViewAll();
            int num = int.Parse(Console.ReadLine());
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            string[] array = File.ReadAllLines(FileHandling.secretFilePath);
            for (int i = 1; i <= num * 3; i++)
            {
                string str = streamReader.ReadLine().Decrypt();
                if (i >= 3 * num - 2 && i <= 3 * num)
                {
                    if (i == 3 * num - 2)
                    {
                        Console.WriteLine("Site : " + str);
                    }
                    else if (i == 3 * num - 1)
                    {
                        Console.WriteLine("username : " + str);
                    }
                    else if (i == 3 * num)
                    {
                        Console.WriteLine("pwd : " + str);
                    }
                    Console.WriteLine("\n want to change ?(ywithpassword / n)");
                    string text = Console.ReadLine();
                    if (text.Length > 1)
                    {
                        FileHandling.IsChange = true;
                        array[i - 1] = text.Substring(1).Encrypt();
                    }
                }
            }
            streamReader.Close();
            if (FileHandling.IsChange)
            {
                File.WriteAllLines(FileHandling.secretFilePath, array);
            }
        }

        [Obsolete]
        public static void InsertOperation()
        {
            string text;
            do
            {
                Console.WriteLine("Enter the name of site:");
                string str = Console.ReadLine().Encrypt();
                Console.WriteLine("USERNAME : ");
                string str2 = Console.ReadLine().Encrypt();
                Console.WriteLine("Enter the PASSWORD :");
                string str3 = Console.ReadLine().Encrypt();
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str);
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str2);
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str3);
                Console.WriteLine("Want to enter more(y/n)");
                text = Console.ReadLine();
            }
            while (!text.Equals("n") && !text.Equals("N"));
        }

        [Obsolete("Use ReadCredential instead")]
        public static void ReadOperation()
        {
            // FileHandling.ViewAll();
            int num = int.Parse(Console.ReadLine());
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            for (int i = 1; i <= num * 3; i++)
            {
                string text = streamReader.ReadLine().Decrypt();
                if (i == 3 * num - 1)
                {
                    Console.WriteLine(" username : " + text + " . copied to clipboard");
                    CommandLine.RunCopy(text);
                    break;
                }
            }
            Console.WriteLine("\n\n Press a key for password . . ");
            Console.ReadKey();
            CommandLine.RunCopy(streamReader.ReadLine().Decrypt());
            CommandLine.RunClear();
            Console.WriteLine("\n\tOK.");
            streamReader.Close();
        }

        #endregion

        #region wrapperToUseExisting
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static void ConvertFileOperation(string oldfile)
        {
            StreamReader streamReader = new StreamReader(oldfile);
            bool flag = true;
            string plaintext;
            while ((plaintext = streamReader.ReadLine()) != null)
            {
                if (flag)
                {
                    File.AppendAllText(FileHandling.secretFilePath, plaintext.Encrypt());
                    flag = false;
                }
                else
                {
                    File.AppendAllText(FileHandling.secretFilePath, "\n" + plaintext.Encrypt());
                }
            }
            streamReader.Close();
        }
        #endregion

    }
}
