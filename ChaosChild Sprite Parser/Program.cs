
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
                        { "Aa", "armscrossed_sad" },
                        { "A2", "armscrossed_happy" },
                        { "A3", "armscrossed_depressed" },
                        { "A4", "armscrossed_pissed" },
                        { "A5", "armscrossed_sad" },
                        { "A6", "armscrossed_happy" },
                        { "A7", "armscrossed_depressed" },
                        { "A8", "armscrossed_pissed" },
                        { "A9", "armscrossed_pissed" },

                        { "Ba", "armsdown_sad" },
                        { "B2", "armsdown_happy" },
                        { "B3", "armsdown_depressed" },
                        { "B4", "armsdown_pissed" },
                        { "B5", "armsdown_sad" },
                        { "B6", "armsdown_happy" },
                        { "B7", "armsdown_depressed" },
                        { "B8", "armsdown_pissed" },
                        { "B9", "armsdown_pissed" },

                        { "Ca", "armscrossed_sad" },
                        { "C2", "armscrossed_happy" },
                        { "C3", "armscrossed_depressed" },
                        { "C4", "armscrossed_pissed" },
                        { "C5", "armscrossed_sad" },
                        { "C6", "armscrossed_happy" },
                        { "C7", "armscrossed_depressed" },
                        { "C8", "armscrossed_pissed" },
                        { "C9", "armscrossed_pissed" },

                        { "Da", "armsdown_sad" },
                        { "D2", "armsdown_happy" },
                        { "D3", "armsdown_depressed" },
                        { "D4", "armsdown_pissed" },
                        { "D5", "armsdown_sad" },
                        { "D6", "armsdown_happy" },
                        { "D7", "armsdown_depressed" },
                        { "D8", "armsdown_pissed" },
                        { "D9", "armsdown_pissed" }
                    }
                },
                {
                    "ARI",
                    new Dictionary<string, string>
                    {
                        { "Aa", "armscrossed_sad" },
                        { "A2", "armscrossed_happy" },
                        { "A3", "armscrossed_depressed" },
                        { "A4", "armscrossed_pissed" }
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

                    string fileName = new FileInfo(filePath).Name;
                    Console.WriteLine(fileName);

                    if (fileName[8].Equals('6'))
                        continue; // skip facial features
                    
                    string[] fileNameNew = new string[5];

                    string characterCode = fileName.Substring(0, 3);




                    //character name

                    fileNameNew[0] = characterCode;




                    //pose + face

                    try
                    {
                        fileNameNew[1] = characterPoseFaceLookup[characterCode][fileName[6].ToString() + fileName[13].ToString()];
                    }
                    catch
                    {
                        fileNameNew[1] = "unknown_unknown";
                    }




                    //size

                    fileNameNew[2] =

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

                    fileNameNew[3] =

                        fileName[15] == '0' ?
                            "silent" :

                        fileName[15] == '1' ?
                            "talking" :
                                    
                        fileName[15] == '2' ?
                            "screaming" :

                        //else
                            "unknown";




                    //clothing

                    fileNameNew[4] =

                        fileName[4] == 'A' ?
                            "school" :

                        fileName[4] == 'B' ?
                            "casual" :

                        //else
                            "unknown";




                    File.Copy(filePath, characterSpritesPathRenamed + "\\" + string.Join("_", fileNameNew) + ".png", true);
                }
            }
        }
    }
}
