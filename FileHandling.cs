
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
        private static string secretFilePath = StartupSettings.CredentialFileName;
        private static bool IsChange = false;

        public static void InsertOperation(Credential credential, ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            String[] contents = { encryptionObj.Encrypt( credential.Description), encryptionObj.Encrypt(credential.UserName), encryptionObj.Encrypt(credential.Pwd) };
            File.AppendAllLines(FileHandling.secretFilePath, contents);
        }

        public static void ExportOperation(Credential credential,ICryptoAlgorithm algo, string Filename)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            String[] contents = { encryptionObj.Encrypt(credential.Description), encryptionObj.Encrypt(credential.UserName), encryptionObj.Encrypt(credential.Pwd) };
            File.AppendAllLines(Filename+".dat", contents);
        }


        public static void ReadCredential(CopyValue copyValue, int credentialSeqNo, ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
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
                    text = encryptionObj.Decrypt( text);
                    // Console.WriteLine(" username : " + text + " . copied to clipboard");
                    CommandLine.RunCopy(text);
                    break;
                }
            }

            thread.Start();
            streamReader.Close();
        }

        public static List<Credential> ViewAll(ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
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
                    currCredential.Description = encryptionObj.Decrypt(encryptedstring);
                }
                else if (num % 3 == 2)
                {
                    currCredential.UserName = encryptionObj.Decrypt(encryptedstring);
                }
                else
                {
                    currCredential.Pwd = encryptionObj.Decrypt(encryptedstring);
                    currCredential.id = num / 3;
                    credentials.Add(currCredential);
                }
                num++;
            }

            streamReader.Close();
            return credentials;
        }

        public static void ModifyOperation(Credential credential,ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            List<string> dataFile = File.ReadAllLines(FileHandling.secretFilePath).ToList<string>();
            // credentials.
            for (int index = 0; index < dataFile.Count; index++)
            {
                if (3 * (credential.id - 1) == index)
                {
                    dataFile[index] = encryptionObj.Encrypt(credential.Description);
                }
                else if (3 * (credential.id - 1) + 1 == index)
                {
                    dataFile[index] = encryptionObj.Encrypt(credential.UserName);
                }
                else if (3 * (credential.id - 1) + 2 == index)
                {
                    dataFile[index] = encryptionObj.Encrypt(credential.Pwd);
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
        public static void DeleteOperation( ICryptoAlgorithm algo)
        {
            Console.WriteLine("Which one to delete ? ");
            FileHandling.ViewAll(algo);
            int num = int.Parse(Console.ReadLine());
            List<string> expr_29 = File.ReadAllLines(FileHandling.secretFilePath).ToList<string>();
            expr_29.RemoveAt(3 * num - 1);
            expr_29.RemoveAt(3 * num - 1);
            expr_29.RemoveAt(3 * num - 1);
            string[] contents = expr_29.ToArray();
            File.WriteAllLines(FileHandling.secretFilePath, contents);
        }

        [Obsolete]
        public static void ModifyOperation(ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            // FileHandling.ViewAll();
            int num = int.Parse(Console.ReadLine());
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            string[] array = File.ReadAllLines(FileHandling.secretFilePath);
            for (int i = 1; i <= num * 3; i++)
            {
                string str = encryptionObj.Decrypt(streamReader.ReadLine());
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
                        array[i - 1] = encryptionObj.Encrypt(text.Substring(1));
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
        public static void InsertOperation(ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            string text;
            do
            {
                Console.WriteLine("Enter the name of site:");
                string str = encryptionObj.Encrypt(Console.ReadLine());
                Console.WriteLine("USERNAME : ");
                string str2 = encryptionObj.Encrypt(Console.ReadLine());
                Console.WriteLine("Enter the PASSWORD :");
                string str3 = encryptionObj.Encrypt(Console.ReadLine());
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str);
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str2);
                File.AppendAllText(FileHandling.secretFilePath, "\n" + str3);
                Console.WriteLine("Want to enter more(y/n)");
                text = Console.ReadLine();
            }
            while (!text.Equals("n") && !text.Equals("N"));
        }

        [Obsolete("Use ReadCredential instead")]
        public static void ReadOperation(ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            // FileHandling.ViewAll();
            int num = int.Parse(Console.ReadLine());
            StreamReader streamReader = new StreamReader(FileHandling.secretFilePath);
            for (int i = 1; i <= num * 3; i++)
            {
                string text = encryptionObj.Decrypt(streamReader.ReadLine());
                if (i == 3 * num - 1)
                {
                    Console.WriteLine(" username : " + text + " . copied to clipboard");
                    CommandLine.RunCopy(text);
                    break;
                }
            }
            Console.WriteLine("\n\n Press a key for password . . ");
            Console.ReadKey();
            CommandLine.RunCopy(encryptionObj.Decrypt(streamReader.ReadLine()));
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

        public static void ConvertFileOperation(string oldfile,ICryptoAlgorithm algo)
        {
            var encryptionObj = new CredentialsEncryption(algo);
            StreamReader streamReader = new StreamReader(oldfile);
            bool flag = true;
            string plaintext;
            while ((plaintext = streamReader.ReadLine()) != null)
            {
                if (flag)
                {
                    File.AppendAllText(FileHandling.secretFilePath, encryptionObj.Encrypt( plaintext));
                    flag = false;
                }
                else
                {
                    File.AppendAllText(FileHandling.secretFilePath, "\n" + encryptionObj.Encrypt(plaintext));
                }
            }
            streamReader.Close();
        }
        #endregion

    }
}
