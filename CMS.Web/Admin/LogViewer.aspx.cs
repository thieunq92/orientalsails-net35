using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using CMS.Web.Admin.UI;

namespace CMS.Web.Admin
{
    public class LogEntry
    {
        public enum IMAGE_TYPE
        {
            DEBUG = 0,
            ERROR = 1,
            FATAL = 2,
            INFO = 3,
            WARN = 4,
            CUSTOM = 5
        }

        public const string IMAGE_DEBUG = "";
        public const string IMAGE_ERROR = "";
        public const string IMAGE_FATAL = "";
        public const string IMAGE_INFO = "";
        public const string IMAGE_WARN = "";
        public const string IMAGE_CUSTOM = "";

        public static string Images(IMAGE_TYPE type)
        {
            switch (type)
            {
                case IMAGE_TYPE.DEBUG:
                    return IMAGE_DEBUG;
                case IMAGE_TYPE.ERROR:
                    return IMAGE_ERROR;
                case IMAGE_TYPE.FATAL:
                    return IMAGE_FATAL;
                case IMAGE_TYPE.INFO:
                    return IMAGE_INFO;
                case IMAGE_TYPE.WARN:
                    return IMAGE_WARN;
                case IMAGE_TYPE.CUSTOM:
                    return IMAGE_CUSTOM;
                default:
                    return IMAGE_INFO;
            }
        }

        private int _Item = 0;
        public int Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private DateTime _TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public DateTime TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }

        private string _Image = Images(IMAGE_TYPE.CUSTOM);
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _Level = string.Empty;
        public string Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private string _Thread = string.Empty;
        public string Thread
        {
            get { return _Thread; }
            set { _Thread = value; }
        }

        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private string _MachineName = string.Empty;
        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value;}
        }

        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _HostName = string.Empty;
        public string HostName
        {
            get { return _HostName; }
            set { _HostName = value; }
        }

        private string _App = string.Empty;
        public string App
        {
            get { return _App; }
            set { _App = value; }
        }

        private string _Throwable = string.Empty;
        public string Throwable
        {
            get { return _Throwable; }
            set { _Throwable = value; }
        }

        private string _Class = string.Empty;
        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }

        private string _Method = string.Empty;
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        private string _File = string.Empty;
        public string File
        {
            get { return _File; }
            set { _File = value; }
        }

        private string _Line = string.Empty;
        public string Line
        {
            get { return _Line; }
            set { _Line = value; }
        }
    }

    public partial class LogViewer : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileName = @"D:\Working\CMS\OrientalSails\Web\log\log.txt";
            LoadFile();
        }

        private string _FileName = string.Empty;
        private string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
            }
        }

        private List<LogEntry> _Entries = new List<LogEntry>();
        public List<LogEntry> Entries
        {
            get { return _Entries; }
            set { _Entries = value; }
        }

        private void Clear()
        {
            //textBoxLevel.Text = string.Empty;
            //textBoxTimeStamp.Text = string.Empty;
            //textBoxMachineName.Text = string.Empty;
            //textBoxThread.Text = string.Empty;
            //textBoxItem.Text = string.Empty;
            //textBoxHostName.Text = string.Empty;
            //textBoxUserName.Text = string.Empty;
            //textBoxApp.Text = string.Empty;
            //textBoxClass.Text = string.Empty;
            //textBoxMethod.Text = string.Empty;
            //textBoxLine.Text = string.Empty;
            //textBoxfile.Text = string.Empty;
            //textBoxMessage.Text = string.Empty;
            //textBoxThrowable.Text = string.Empty;
        }

        private void OpenFile(string fileName)
        {
            FileName = fileName;
            LoadFile();
        }

        private void LoadFile()
        {
            //textboxFileName.Text = FileName;
            List<LogEntry> entries = new List<LogEntry>();
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string sXml = string.Empty;
            string sBuffer = string.Empty;
            int iIndex = 1;

            Clear();

            try
            {
                FileStream oFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                StreamReader oStreamReader = new StreamReader(oFileStream);
                sBuffer = string.Format("<root>{0}</root>", oStreamReader.ReadToEnd());
                oStreamReader.Close();
                oFileStream.Close();

                #region Read File Buffer
                ////////////////////////////////////////////////////////////////////////////////
                StringReader oStringReader = new StringReader(sBuffer);
                XmlTextReader oXmlTextReader = new XmlTextReader(oStringReader);
                oXmlTextReader.Namespaces = false;
                while (oXmlTextReader.Read())
                {
                    if ((oXmlTextReader.NodeType == XmlNodeType.Element) && (oXmlTextReader.Name == "log4j:event"))
                    {
                        LogEntry logentry = new LogEntry();

                        logentry.Item = iIndex;

                        double dSeconds = Convert.ToDouble(oXmlTextReader.GetAttribute("timestamp"));
                        logentry.TimeStamp = dt.AddMilliseconds(dSeconds).ToLocalTime();
                        logentry.Thread = oXmlTextReader.GetAttribute("thread");

                        #region get level
                        ////////////////////////////////////////////////////////////////////////////////
                        logentry.Level = oXmlTextReader.GetAttribute("level");
                        switch (logentry.Level)
                        {
                            case "ERROR":
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.ERROR);
                                    break;
                                }
                            case "INFO":
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.INFO);
                                    break;
                                }
                            case "DEBUG":
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.DEBUG);
                                    break;
                                }
                            case "WARN":
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.WARN);
                                    break;
                                }
                            case "FATAL":
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.FATAL);
                                    break;
                                }
                            default:
                                {
                                    logentry.Image = LogEntry.Images(LogEntry.IMAGE_TYPE.CUSTOM);
                                    break;
                                }
                        }
                        ////////////////////////////////////////////////////////////////////////////////
                        #endregion

                        #region read xml
                        ////////////////////////////////////////////////////////////////////////////////
                        while (oXmlTextReader.Read())
                        {
                            if (oXmlTextReader.Name == "log4j:event")   // end element
                                break;
                            else
                            {
                                switch (oXmlTextReader.Name)
                                {
                                    case ("log4j:message"):
                                        {
                                            logentry.Message = oXmlTextReader.ReadString();
                                            break;
                                        }
                                    case ("log4j:data"):
                                        {
                                            switch (oXmlTextReader.GetAttribute("name"))
                                            {
                                                case ("log4jmachinename"):
                                                    {
                                                        logentry.MachineName = oXmlTextReader.GetAttribute("value");
                                                        break;
                                                    }
                                                case ("log4net:HostName"):
                                                    {
                                                        logentry.HostName = oXmlTextReader.GetAttribute("value");
                                                        break;
                                                    }
                                                case ("log4net:UserName"):
                                                    {
                                                        logentry.UserName = oXmlTextReader.GetAttribute("value");
                                                        break;
                                                    }
                                                case ("log4japp"):
                                                    {
                                                        logentry.App = oXmlTextReader.GetAttribute("value");
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case ("log4j:throwable"):
                                        {
                                            logentry.Throwable = oXmlTextReader.ReadString();
                                            break;
                                        }
                                    case ("log4j:locationInfo"):
                                        {
                                            logentry.Class = oXmlTextReader.GetAttribute("class");
                                            logentry.Method = oXmlTextReader.GetAttribute("method");
                                            logentry.File = oXmlTextReader.GetAttribute("file");
                                            logentry.Line = oXmlTextReader.GetAttribute("line");
                                            break;
                                        }
                                }
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////
                        #endregion

                        entries.Add(logentry);
                        iIndex++;
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////
                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            //this.listView1.ItemsSource =  entries;
            rptLogs.DataSource = entries;
            rptLogs.DataBind();
        }

        #region ListView Events
        ////////////////////////////////////////////////////////////////////////////////
        //private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
                //Clear();
                //LogEntry logentry = this.listView1.SelectedItem as LogEntry;

                //this.image1.Source = logentry.Image;
                //this.textBoxLevel.Text = logentry.Level;
                //this.textBoxTimeStamp.Text = string.Format("{0} {1}", logentry.TimeStamp.ToShortDateString(), logentry.TimeStamp.ToShortTimeString());
                //this.textBoxMachineName.Text = logentry.MachineName;
                //this.textBoxThread.Text = logentry.Thread;
                //this.textBoxItem.Text = logentry.Item.ToString();
                //this.textBoxHostName.Text = logentry.HostName;
                //this.textBoxUserName.Text = logentry.UserName;
                //this.textBoxApp.Text = logentry.App;
                //this.textBoxClass.Text = logentry.Class;
                //this.textBoxMethod.Text = logentry.Method;
                //this.textBoxLine.Text = logentry.Line;
                //this.textBoxMessage.Text = logentry.Message;
                //this.textBoxThrowable.Text = logentry.Throwable;
                //this.textBoxfile.Text = logentry.File;
        //    }
        //    catch { }
        //}

        private ListSortDirection _Direction = ListSortDirection.Descending;

        //private void ListView1_HeaderClicked(object sender, RoutedEventArgs e)
        //{
            //GridViewColumnHeader header = e.OriginalSource as GridViewColumnHeader;
            //ListView source = e.Source as ListView;
            //try
            //{
            //    ICollectionView dataView = CollectionViewSource.GetDefaultView(source.ItemsSource);
            //    dataView.SortDescriptions.Clear();
            //    _Direction = _Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            //    SortDescription description = new SortDescription(header.Content.ToString(), _Direction);
            //    dataView.SortDescriptions.Add(description);
            //    dataView.Refresh();
            //}
            //catch (Exception)
            //{
            //}
        //}
        ////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region DragDrop
        ////////////////////////////////////////////////////////////////////////////////
        private delegate void VoidDelegate();
        //private void listView1_Drop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        try
        //        {
        //            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
        //            if (a != null)
        //            {
        //                FileName = a.GetValue(0).ToString();
        //                Dispatcher.BeginInvoke(
        //                    DispatcherPriority.Background, 
        //                    new VoidDelegate(delegate { LoadFile(); })
        //                    );
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error in Drag Drop: " + ex.Message);
        //        }
        //    }
        //}

        ////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Menu Events
        ////////////////////////////////////////////////////////////////////////////////

        //private void MenuRefresh_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadFile();
        //}

        //private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void MenuAbout_Click(object sender, RoutedEventArgs e)
        //{
        //    About about = new About();
        //    about.ShowDialog();
        //}
        ////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
