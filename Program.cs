using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileMover
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                string curDir;
                Console.WriteLine("Current Dir: ");
                curDir = Console.ReadLine();
                //newDir is main folder location
                //newPath is subfolder to move to
                Console.WriteLine("New Directory, (leave blank for creation at curDir...WIP might stick in base root)");
                string newDir;
                string input = Console.ReadLine();
                if (input == "") { newDir = curDir; } else { newDir = input; }
                string newPath;
                Console.WriteLine("Input file extension, png jpg etc.");
                string fileExt = "*." + Console.ReadLine();


                string sortBy = "Creation";
                Console.WriteLine("Input: Creation, LastWrite");
                sortBy = Console.ReadLine();

                //Selection should be all files with correct extentions
                var selection = Directory.GetFiles(curDir, fileExt, SearchOption.AllDirectories);

                foreach (var fle in selection)
                {
                    Console.WriteLine("File Found: " + fle);
                    //
                    DateTime lastWrite = File.GetLastWriteTime(fle);
                    DateTime creationdate = File.GetCreationTime(fle);

                    Console.WriteLine(lastWrite.ToString());

                    //Update newPath as the new subfolder
                    char[] delim = { '/', ' ' };
            

                    switch (sortBy.ToLower())
                    {
                        case "creation":
                        case "c":
                            string[] Createsplit = creationdate.ToString().Split(delim);
                            
                            //year, month subfolder
                            newPath = newDir + '\\' + Createsplit[2] + '\\' + Createsplit[0];
                            //create newpath in destination

                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            // update dest with new name
                            string CreatedestPath = newPath + '\\' + Path.GetFileName(fle);
                            //Move file  might need to check for error like already exists

                            if (!File.Exists(CreatedestPath)) { File.Move(fle, CreatedestPath); }
                            break;
                        case "":
                            break;

                        case "lastwrite":
                        case "last":
                        case "l":
                            string[] split = lastWrite.ToString().Split(delim);
                            //year, month subfolder
                            newPath = newDir + '\\' + split[2] + '\\' + split[0];
                            //create newpath in destination

                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            // update dest with new name
                            string destPath = newPath + '\\' + Path.GetFileName(fle);
                            //Move file  might need to check for error like already exists

                            if (!File.Exists(destPath)) { File.Move(fle, destPath); }
                            

                            break;
                        default:
                            break;
                    }
                    
                }

                //Finish, show number of moved files
                Console.WriteLine("Finished moving " + fileExt + " files to " + newDir);
                Console.WriteLine("Loop? (y) for yes, anything else for no");
                input = Console.ReadLine();
                if (input == "y")
                {
                    loop = true;
                }
                else { loop = false; }
            }
        }
    }
}

