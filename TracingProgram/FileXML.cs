using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Security;
using System.Drawing;
using System.Text;

namespace TracingProgram
{
    class FileXML
    {
        public int widthGrid { private set; get; }
        public int heightGrid { private set; get; }
        List<Element> chips = new List<Element>();
        public Layer layer { private set; get; }
        int sizeCell = 16;
        public string name { private set; get; }
        /// <summary>
        /// Считывает информацию с XML файла о дискретном поле
        /// содержит высоту, ширину поля, набор элементов и первый слой с очередью трассировки
        /// </summary>
        /// <param name="fileName">Имя XML файла</param>
        /// <param name="sizeCell">Размер одной ячейки дискретного поля, по умолчанию 16</param>
        /// <exception cref="ArgumentException"/>
        public FileXML(string fileName, int sizeCell, Graphics g)
        {
            this.name = fileName;
            this.sizeCell = sizeCell;
            readFile(fileName, g);
        }

        private void readFile(string filename, Graphics g)
        {
            try
            {
                FileStream file = new FileStream(filename, FileMode.Open);
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlElement root = doc.DocumentElement;
                this.widthGrid = Int32.Parse(root.Attributes.GetNamedItem("width").Value);
                this.heightGrid = Int32.Parse(root.Attributes.GetNamedItem("height").Value);
                this.layer = new Layer(heightGrid, widthGrid, 1);
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "Elements")
                    {
                        foreach (XmlNode el in node.ChildNodes)
                        {
                            int x = Int32.Parse(el.Attributes.GetNamedItem("x").Value);
                            int y = Int32.Parse(el.Attributes.GetNamedItem("y").Value);
                            string name = el.Attributes.GetNamedItem("Name").Value;
                            switch (el.Name.ToLower())
                            {
                                case "dip14":
                                    chips.Add(new Dip14(x, y, this.sizeCell, name));
                                    break;
                                case "dip16":
                                    chips.Add(new Dip16(x, y, this.sizeCell, name));
                                    break;
                                case "dip24":
                                    chips.Add(new Dip24(x, y, this.sizeCell, name));
                                    break;
                                case "contact":
                                    chips.Add(new Contact(x, y, this.sizeCell, 1, name));
                                    break;
                            }
                            chips.Last().draw(g);
                        }
                    }
                    else if (node.Name == "Connections")
                    {
                        foreach (XmlNode con in node.ChildNodes)
                        {
                            int i = Int32.Parse(con.Attributes.GetNamedItem("contact").Value);
                            QueueTrace tmp = new QueueTrace(this.getContactOfChip(con.Name, i));
                            foreach (XmlNode dst in con.ChildNodes)
                            {
                                i = Int32.Parse(dst.Attributes.GetNamedItem("contact").Value);
                                tmp.Add(this.getContactOfChip(dst.Name, i));
                            }
                            layer.queue = new QueueTrace(tmp);
                        }
                    }
                }
                layer.fillField(chips);
                file.Close();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Ошибка: Аргумент\nПодробнее: " + ex.Message);
            }
            catch (SecurityException ex)
            {
                throw new ArgumentException("Ошибка: Безопасность\nПодробнее: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                throw new ArgumentException("Ошибка: Не найден файл\nПодробнее: " + ex.Message);
            }
            catch (IOException ex)
            {
                throw new ArgumentException("Ошибка: Ввод\\Вывод\nПодробнее: " + ex.Message);
            }
            catch (XmlException ex)
            {
                throw new ArgumentException("Ошибка: XML\nПодробнее: " + ex.Message);
            }
        }

        public Point getContactOfChip(string name, int number)
        {
            foreach (Element chip in chips)
            {
                if (name == chip.Name)
                {
                    if (chip is Chip)
                    {
                        return (chip as Chip).getContactPoint(number);
                    }
                    else if (chip is Contact)
                    {
                        return new Point((chip as Contact).x, (chip as Contact).y);
                    }
                }
            }
            throw new ArgumentException("Элемента с именем " + name + " нет на плате.\nПроверьте конфигурационный файл");
        }

        public void CopyChips(out List<Element> list)
        {
            list = new List<Element>();
            list.AddRange(chips);
        }
    }
}
