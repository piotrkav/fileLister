using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PTLAB1
{
    class Tree
    {
        public static DateTime date;
        public SortedList<String, int> listOfElements = new SortedList<string, int>(new MyComparer());

        public int NumberOfElements { get; set; }

        public Tree(string root, string prefix)
        {
            DirectoryInfo mDir = new DirectoryInfo(root);
            FileInfo[] files = mDir.GetFiles();
            DirectoryInfo[] folders = mDir.GetDirectories();

            int size = folders.Length + files.Length;
            this.NumberOfElements = size;
            Console.WriteLine(prefix + mDir.Name + "(" + size + " elements" + ")  " + mDir.GetRahs());
       

            foreach (var item in files)
            {

                listOfElements.Add(item.ToString(), (int)item.Length);

                if (date < item.LastWriteTime)
                {
                    date = item.LastWriteTime;
                }

                

                Console.WriteLine(prefix + "    " + item.ToString() + "(" + item.Length + " bytes)" + "   " + item.GetRahs());
            }
            foreach (var item in folders)
            {

                string temp = item.FullName;
                Tree newTree = new Tree(temp, prefix + "    ");
                listOfElements.Add(item.ToString(), newTree.NumberOfElements);
            }


        }

        public void Serialize()
        {
            FileStream mFileStream = new FileStream("file.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(mFileStream, this.listOfElements);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                mFileStream.Close();
            }

        }

        public void Deserialize()
        {

            SortedList<string, int> list = new SortedList<string, int>(new MyComparer());

            FileStream mFileStream = new FileStream("file.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                list = (SortedList<string, int>)formatter.Deserialize(mFileStream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                mFileStream.Close();
            }

            foreach (KeyValuePair<string, int> item in list)
            {
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            }
        }
    }

    static class Extensions
    {
        public static string GetRahs(this FileInfo x)
        {
            FileAttributes attr = File.GetAttributes(x.FullName);
            String temp = "";
            if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                temp += "r";
            else
                temp += "-";
            if ((attr & FileAttributes.Archive) == FileAttributes.Archive)
                temp += "a";
            else
                temp += "-";
            if ((attr & FileAttributes.Hidden) == FileAttributes.Hidden)
                temp += "h";
            else
                temp += "-";
            if ((attr & FileAttributes.System) == FileAttributes.System)
                temp += "s";
            else
                temp += "-";

            return temp;
        }

        public static string GetRahs(this DirectoryInfo x)
        {
            FileAttributes attr = File.GetAttributes(x.FullName);
            String temp = "";
            if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                temp += "r";
            else
                temp += "-";
            if ((attr & FileAttributes.Archive) == FileAttributes.Archive)
                temp += "a";
            else
                temp += "-";
            if ((attr & FileAttributes.Hidden) == FileAttributes.Hidden)
                temp += "h";
            else
                temp += "-";
            if ((attr & FileAttributes.System) == FileAttributes.System)
                temp += "s";
            else
                temp += "-";

            return temp;
        }
    }
}
