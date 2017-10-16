using System;



namespace VersionControl
{
    class Program
    {
        /* Takes three or two arguments, file path, file name, and version change.
            If no file path is specified it will assume the file is in the same directory as the application.
         */
        static void Main(string[] args)
        {
            VersionController versionController = new VersionController();
            versionController.init(args[0]);
            versionController.run(args[1]);
        }
    }
}
