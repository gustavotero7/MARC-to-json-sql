using MARC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jonas_Pre
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] marcs = Directory.GetFiles("d:/Marcs/");

            for (int i = 0; i < marcs.Length; i++)
            {
                //MarcsToSQL(marcs[i]);
                MarcsToJson(marcs[i]);
            }

            Console.ReadKey();

        }

        public static void MarcsToSQL(string filePath)
        {
            //Read raw MARC record from a file. 
            //string rawMarc = File.ReadAllText("d:/Marcs/BIBLIOGRAPHIC_11957277400003841_6.xml");
            string rawMarc = File.ReadAllText(filePath);
            FileMARCXml marcRecords = new FileMARCXml(rawMarc);

            int i = 0;

            string query = "INSERT INTO babel.books (title, author, topic, note, location) VALUES \n";

            foreach (Record item in marcRecords)
            {
                //Console.WriteLine("Record " + item);
                query += i == 0 ? "" : ",\n";
                string queryValue = "(";
                Field f245 = item["245"];
                if (f245 != null) if (f245.IsDataField())
                    {
                        DataField s245 = (DataField)f245;
                        string title = "";
                        title += (s245['a'] == null) ? "" : s245['a'].Data;
                        title += (s245['b'] == null) ? "" : " " + s245['b'].Data;
                        //Console.WriteLine(title);
                        queryValue += "'"+title.Replace("Host record for: ","").Replace("'", "''") + "',";
                    }
                    else queryValue += "'',";
                else queryValue += "'',";

                Field f100 = item["100"];
                if (f100 != null) if (f100.IsDataField())
                    {
                        DataField s100 = (DataField)f100;
                        string author = "";
                        author += (s100['a'] == null) ? "" : s100['a'].Data;
                        author += (s100['b'] == null) ? "" : " " + s100['b'].Data;
                        author += (s100['c'] == null) ? "" : " " + s100['c'].Data;
                        author += (s100['d'] == null) ? "" : " " + s100['d'].Data;
                        //Console.WriteLine(author);
                        queryValue += "'" + author.Replace("'", "''") + "',";
                    }
                    else queryValue += "'',";
                else queryValue += "'',";

                Field f650 = item["650"];
                if (f650 != null) if (f650.IsDataField())
                    {
                        DataField s650 = (DataField)f650;
                        string topic = "";
                        topic += (s650['a'] == null) ? "" : s650['a'].Data;
                        topic += (s650['b'] == null) ? "" : " " + s650['b'].Data;
                        topic += (s650['c'] == null) ? "" : " " + s650['c'].Data;
                        topic += (s650['d'] == null) ? "" : " " + s650['e'].Data;
                        //Console.WriteLine(topic);
                        queryValue += "'" + topic.Replace("'", "''") + "',";
                    }
                    else queryValue += "'',";
                else queryValue += "'',";

                Field f500 = item["500"];
                if (f500 != null) if (f500.IsDataField())
                    {
                        DataField s500 = (DataField)f500;
                        string note = "";
                        note += (s500['a'] == null) ? "" : s500['a'].Data;
                        //Console.WriteLine(note);
                        queryValue += "'" + note.Replace("'", "''") + "',";
                    }
                    else queryValue += "'',";
                else queryValue += "'',";

                Field f852 = item["852"];
                if (f852 != null) if (f852.IsDataField())
                    {
                        DataField s852 = (DataField)f852;
                        string location = "";
                        location += (s852['a'] == null) ? "" : s852['a'].Data;
                        location += (s852['b'] == null) ? "" : " " + s852['b'].Data;
                        location += (s852['c'] == null) ? "" : " " + s852['c'].Data;
                        location += (s852['e'] == null) ? "" : " " + s852['e'].Data;
                        location += (s852['h'] == null) ? "" : " " + s852['h'].Data;
                        //Console.WriteLine(location);
                        queryValue += "'" + location.Replace("'","''") + "'";
                    }
                    else queryValue += "''";
                else queryValue += "''";

                queryValue += ")";

                query += queryValue;

                /*if (i > 100)
                    break;*/
                Console.WriteLine("*****RECORD "+ i.ToString() +" CREATED*****");
                i++;
            }

            System.IO.File.WriteAllText(filePath+".sql", query);
            //Console.WriteLine(query);
        }


        public static void MarcsToJson(string filePath)
        {
            string rawMarc = File.ReadAllText(filePath);
            FileMARCXml marcRecords = new FileMARCXml(rawMarc);

            int i = 0;

            //string query = "INSERT INTO babel.books (title, author, topic, note, location) VALUES \n";
            string json = "[";

            foreach (Record item in marcRecords)
            {
                json += i == 0 ? "" : ",\n";
                Field f245 = item["245"];
                string _title = "";
                if (f245 != null) if (f245.IsDataField())
                    {
                        DataField s245 = (DataField)f245;
                        string title = "";
                        title += (s245['a'] == null) ? "" : s245['a'].Data;
                        title += (s245['b'] == null) ? "" : " " + s245['b'].Data;

                        _title = title.Replace("Host record for: ", "");
                    }

                string _author = "";
                Field f100 = item["100"];
                if (f100 != null) if (f100.IsDataField())
                    {
                        DataField s100 = (DataField)f100;
                        string author = "";
                        author += (s100['a'] == null) ? "" : s100['a'].Data;
                        author += (s100['b'] == null) ? "" : " " + s100['b'].Data;
                        author += (s100['c'] == null) ? "" : " " + s100['c'].Data;
                        author += (s100['d'] == null) ? "" : " " + s100['d'].Data;
                        //queryValue += "'" + author.Replace("'", "''") + "',";
                        _author = author;
                    }

                string _topic = "";
                Field f650 = item["650"];
                if (f650 != null) if (f650.IsDataField())
                    {
                        DataField s650 = (DataField)f650;
                        string topic = "";
                        topic += (s650['a'] == null) ? "" : s650['a'].Data;
                        topic += (s650['b'] == null) ? "" : " " + s650['b'].Data;
                        topic += (s650['c'] == null) ? "" : " " + s650['c'].Data;
                        topic += (s650['d'] == null) ? "" : " " + s650['e'].Data;
                        //Console.WriteLine(topic);
                        //queryValue += "'" + topic.Replace("'", "''") + "',";
                        _topic = topic;
                    }

                string _note = "";
                Field f500 = item["500"];
                if (f500 != null) if (f500.IsDataField())
                    {
                        DataField s500 = (DataField)f500;
                        string note = "";
                        note += (s500['a'] == null) ? "" : s500['a'].Data;
                        //Console.WriteLine(note);
                        //queryValue += "'" + note.Replace("'", "''") + "',";
                        _note = note;
                    }

                string _location = "";
                Field f852 = item["852"];
                if (f852 != null) if (f852.IsDataField())
                    {
                        DataField s852 = (DataField)f852;
                        string location = "";
                        location += (s852['a'] == null) ? "" : s852['a'].Data;
                        location += (s852['b'] == null) ? "" : " " + s852['b'].Data;
                        location += (s852['c'] == null) ? "" : " " + s852['c'].Data;
                        location += (s852['e'] == null) ? "" : " " + s852['e'].Data;
                        location += (s852['h'] == null) ? "" : " " + s852['h'].Data;
                        //Console.WriteLine(location);
                        //queryValue += "'" + location.Replace("'", "''") + "'";
                        _location = location;
                    }

                string jsonObject = "{\"title\":\""+ _title.Replace("\"", "ˮ")+ "\",\"author\":\""+ _author.Replace("\"", "ˮ") + "\",\"topic\":\""+ _topic.Replace("\"", "ˮ") + "\",\"note\":\""+ _note.Replace("\"", "ˮ") + "\",\"location\":\""+ _location.Replace("\"", "ˮ") + "\"}";
                json += jsonObject;

                //query += queryValue;

                /*if (i > 100)
                    break;*/
                Console.WriteLine("*****RECORD " + i.ToString() + " CREATED*****");
                i++;
            }
            json += "]";
            System.IO.File.WriteAllText(filePath + ".json", json);
        }
    }


}
