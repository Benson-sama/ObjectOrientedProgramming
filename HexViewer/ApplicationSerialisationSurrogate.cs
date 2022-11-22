namespace HexViewer
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class ApplicationSerialisationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CurrentFileTabIndex", ((Application)obj).CurrentFileTabIndex);

            int i = 0;
            foreach (var tab in ((Application)obj).fileTabs)
            {
                info.AddValue($"PathOfFileTab{i}", tab.Path);
                info.AddValue($"IndexOfFileTab{i}", tab.Lines[0].Offset);
                i++;
            }

            info.AddValue("NumberOfFileTabs", i);
        }

        public object SetObjectData(object obj, SerializationInfo info,
                                    StreamingContext context, ISurrogateSelector selector)
        {
            Application application = new Application();

            List<FileTab> fileTabs = new List<FileTab>();
            int numberOfFileTabs = info.GetInt32("NumberOfFileTabs");

            for (int i = 0; i < numberOfFileTabs; i++)
            {
                string path = info.GetString($"PathOfFileTab{i}");
                int index = info.GetInt32($"IndexOfFileTab{i}");

                FileTab fileTab = new FileTab(path);
                fileTab.ReadFromFile(index);
                fileTabs.Add(fileTab);
            }

            application.fileTabs = fileTabs;
            application.CurrentFileTabIndex = info.GetInt32("CurrentFileTabIndex");

            return application;
        }
    }
}