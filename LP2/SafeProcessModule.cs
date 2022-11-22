namespace LP2
{
    using System;
    using System.Diagnostics;

    public class SafeProcessModule
    {
        public SafeProcessModule(Process process)
        {
            try
            {
                this.FileName = process.MainModule.FileName;
            }
            catch (Exception)
            {
                this.FileName = "no permission";
            }
        }

        public string FileName
        {
            get;
            set;
        }
    }
}