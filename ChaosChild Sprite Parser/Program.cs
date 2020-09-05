
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChaosChild_Sprite_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var characterPoseFaceLookup = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "KUN",
                    new Dictionary<string, string>
                    {
                        { "Aa", "armscrossed\\sad" },
                        { "A2", "armscrossed\\happy" },
                        { "A3", "armscrossed\\depressed" },
                        { "A4", "armscrossed\\pissed" },
                        { "A5", "armscrossed\\sad" },
                        { "A6", "armscrossed\\happy" },
                        { "A7", "armscrossed\\depressed" },
                        { "A8", "armscrossed\\pissed" },
                        { "A9", "armscrossed\\pissed" },

                        { "Ba", "armsdown\\sad" },
                        { "B2", "armsdown\\happy" },
                        { "B3", "armsdown\\depressed" },
                        { "B4", "armsdown\\pissed" },
                        { "B5", "armsdown\\sad" },
                        { "B6", "armsdown\\happy" },
                        { "B7", "armsdown\\depressed" },
                        { "B8", "armsdown\\pissed" },
                        { "B9", "armsdown\\pissed" },

                        { "Ca", "armscrossed\\sad" },
                        { "C2", "armscrossed\\happy" },
                        { "C3", "armscrossed\\depressed" },
                        { "C4", "armscrossed\\pissed" },
                        { "C5", "armscrossed\\sad" },
                        { "C6", "armscrossed\\happy" },
                        { "C7", "armscrossed\\depressed" },
                        { "C8", "armscrossed\\pissed" },
                        { "C9", "armscrossed\\pissed" },

                        { "Da", "armsdown\\sad" },
                        { "D2", "armsdown\\happy" },
                        { "D3", "armsdown\\depressed" },
                        { "D4", "armsdown\\pissed" },
                        { "D5", "armsdown\\sad" },
                        { "D6", "armsdown\\happy" },
                        { "D7", "armsdown\\depressed" },
                        { "D8", "armsdown\\pissed" },
                        { "D9", "armsdown\\pissed" }
                    }
                },
                {
                    "ARI",
                    new Dictionary<string, string>
                    {
                        { "Aa", "armscrossed\\sad" },
                        { "A2", "armscrossed\\happy" },
                        { "A3", "armscrossed\\depressed" },
                        { "A4", "armscrossed\\pissed" }
                    }
                }
            };

            string characterSpritesPathSource = @"C:\tmp\SpritesSource";
            string characterSpritesPathRenamed = @"C:\tmp\SpritesRenamed";

            foreach (string directory in Directory.GetDirectories(characterSpritesPathSource))
            {
                foreach (string filePath in Directory.GetFiles(directory))
                {
                    //ARI_ALA_40000a00.png
                    //ARI_ALA_40000100.png
                    //ARI_ALA_40000200.png

                    int i = 0;

                    string fileName = new FileInfo(filePath).Name;
                    Console.WriteLine(fileName);

                    if (fileName[8].Equals('6'))
                        continue; // skip facial features
                    
                    string[] subFilePathTarget = new string[5];

                    string characterCode = fileName.Substring(0, 3);




                    //character name

                    subFilePathTarget[i++] = characterCode;




                    //pose + face + clothing

                    string clothing =

                        fileName[4] == 'A' ?
                            "school" :

                        fileName[4] == 'B' ?
                            "casual" :

                            //else
                            "unknown";

                    try
                    {
                        subFilePathTarget[i] = characterPoseFaceLookup[characterCode][fileName[6].ToString() + fileName[13].ToString()];


                        var subFilePathTargetParts = subFilePathTarget[i].Split("\\");
                        subFilePathTargetParts[1] = clothing + "_" + subFilePathTargetParts[1];

                        subFilePathTarget[i++] = string.Join("\\", subFilePathTargetParts);
                    }
                    catch
                    {
                        subFilePathTarget[i++] = "pose" + fileName[6].ToString() + "\\" + clothing + "_face" + fileName[13].ToString();
                    }




                    //size

                    subFilePathTarget[i++] =

                        fileName[5] == 'X' ?
                            "closeup" :

                        fileName[5] == 'L' ?
                            "large" :

                        fileName[5] == 'M' ?
                            "medium" :

                        fileName[5] == 'S' ?
                            "small" :

                        //else
                            "unknown";




                    //mouth open width

                    subFilePathTarget[i++] =

                        fileName[15] == '0' ?
                            "silent" :

                        fileName[15] == '1' ?
                            "talking" :
                                    
                        fileName[15] == '2' ?
                            "screaming" :

                        //else
                            "unknown";



                    string subFilePathTargetStr = string.Join("_", subFilePathTarget) + ".png";
                    string subFolderPathTargetStr = subFilePathTargetStr.Split("\\")[0];

                    Directory.CreateDirectory(characterSpritesPathRenamed + "\\" + subFolderPathTargetStr);
                    File.Copy(filePath, characterSpritesPathRenamed + "\\" + subFilePathTargetStr, true);
                }
            }
        }
    }
}
