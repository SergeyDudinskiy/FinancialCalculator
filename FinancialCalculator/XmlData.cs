using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FinancialCalculator
{
    class XmlData
    {
        public static string GetRate()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream fs;
            bool flag = true;
            string errorString = "";

            try
            {
                fs = new FileStream(Directory.GetCurrentDirectory() + "\\СurrencyRate.xml", FileMode.Open);
                xmlDoc.Load(fs);
                fs.Close();
            }
            catch
            {
                flag = false;
                errorString = "Ошибка при чтении файла с курсами валют!";
            }            

            if (flag == true)
            {
                try
                {
                    Converter.USDToEUR = Convert.ToDouble(xmlDoc.GetElementsByTagName("USDToEUR")[0].InnerText);
                    Converter.EURToUSD = Convert.ToDouble(xmlDoc.GetElementsByTagName("EURToUSD")[0].InnerText);
                    Converter.USDToRUB = Convert.ToDouble(xmlDoc.GetElementsByTagName("USDToRUB")[0].InnerText);
                    Converter.RUBToUSD = Convert.ToDouble(xmlDoc.GetElementsByTagName("RUBToUSD")[0].InnerText);
                    Converter.EURToRUB = Convert.ToDouble(xmlDoc.GetElementsByTagName("EURToRUB")[0].InnerText);
                    Converter.RUBToEUR = Convert.ToDouble(xmlDoc.GetElementsByTagName("RUBToEUR")[0].InnerText);
                }
                catch
                {
                    errorString = "Ошибка при чтении файла с курсами валют!";
                }
            }

            return errorString;
        }

        public static string SaveData(string operation, string result)
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream fs;
            bool flag = true;
            string errorString = "";
            int maxId = 0;

            try
            {
                fs = new FileStream(Environment.CurrentDirectory.ToString() + "\\Log.xml", FileMode.Open);
                xmlDoc.Load(fs);
                fs.Close();
            }
            catch
            {
                flag = false;
            }
            
            if (flag == false)
            {
                XmlDeclaration documentType = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(documentType);
                XmlNode rootNode = xmlDoc.CreateElement("", "Operations", ""); 
                xmlDoc.AppendChild(rootNode);
            }
            else
            {
                XmlNodeList list = xmlDoc.GetElementsByTagName("Operation");
                List<int> idList = new List<int>();
                
                if (list.Count > 0)
                {
                    try
                    {
                        int id = 0;

                        for (int i = 0; i < list.Count; i++)
                        {
                            XmlElement eO = (XmlElement)xmlDoc.GetElementsByTagName("Operation")[i];
                            id = Convert.ToInt32(eO.GetAttribute("Id"));
                            idList.Add(id);
                        }

                        foreach (int j in idList)
                        {
                            if (j > maxId)
                            {
                                maxId = j;
                            }
                        }

                        maxId++;
                    }
                    catch
                    {

                    }
                }
            }

            XmlElement eOperation = xmlDoc.CreateElement("Operation");
            eOperation.SetAttribute("Id", maxId.ToString());

            XmlElement eExpression = xmlDoc.CreateElement("Expression");
            XmlElement eResult = xmlDoc.CreateElement("Result");

            XmlText tExpression = xmlDoc.CreateTextNode(operation);
            XmlText tResult = xmlDoc.CreateTextNode(result);

            eExpression.AppendChild(tExpression);
            eResult.AppendChild(tResult);

            eOperation.AppendChild(eExpression);
            eOperation.AppendChild(eResult);

            xmlDoc.DocumentElement.AppendChild(eOperation);

            try
            {
                xmlDoc.Save(Environment.CurrentDirectory.ToString() + "\\Log.xml");
            }
            catch
            {
                flag = false;
                errorString = "Ошибка при записи данных в файл с валютными операциями!";
            }

            return errorString;
        }
    }
}
