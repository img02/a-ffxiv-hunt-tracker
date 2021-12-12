using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Models.ReadResults;

namespace untitled_ffxiv_hunt_tracker.Memory
{
    public class ChatLogReader
    {
        private readonly MemoryHandler _memoryHandler;

        private int _previousArrayIndex = 0;
        private int _previousOffset = 0;

        //these 2 could be moved into the readChatLog() method? if not used elsewhere
        public ChatLogResult readResult { get; set; }

        public List<ChatLogItem> chatLogEntries { get; set; }

        /*_previousArrayIndex = readResult.PreviousArrayIndex;
        _previousOffset = readResult.PreviousOffset;*/

        public ChatLogReader(MemoryHandler memoryHandler)
        {
            _memoryHandler = memoryHandler;
        }

        //loop this and use event?
        public void ReadChatLog()
        {
            readResult = _memoryHandler.Reader.GetChatLog(_previousArrayIndex, _previousOffset);
            chatLogEntries = readResult.ChatLogItems.ToList();
            _previousArrayIndex = readResult.PreviousArrayIndex;
            _previousOffset = readResult.PreviousOffset;


            //compare log lines to the system message CODE: Added new combatant xxx yyy zzz etc
            //compare combatant name / id / hp / map territory / etc as needed to verify mob.
            //then invoke event that to notify other objects to add this mob to the list?
            // OR do all that in the memreader getMob / getactors thing lmao.

            //use this to check to COMMANDS typed in chat,
            //and / or Welcome to WORLD. - to verify what world the user is in?

            
            if (chatLogEntries.Count > 0)
            {
                #region DEBUG
                int count = 0;
                if (chatLogEntries.FirstOrDefault(c => c.Combined.Contains("hello")) != null)
                {
                    foreach (var log in chatLogEntries)
                    {
                        if (log.Combined.Contains("hello"))
                        {
                            count++;
                            Console.WriteLine(log.Message);
                        }
                    }
                    //Console.WriteLine($"hello - {count}");
                    ChatLogEvent?.Invoke(this,count.ToString());
                }
                #endregion

                //if (chatLogEntries.FirstOrDefault(c => c.Combined.Contains("/train")) != null)
                if (chatLogEntries.Count > 0)
                {
                    foreach (var log in chatLogEntries)
                    {
                        if (log.Message.Contains("/train"))
                        {
                            Console.WriteLine(log.Combined);
                            ChatLogCommandCreateTrain?.Invoke(this,
                                Regex.Match(log.Combined, "^003C:.+/train (.+?) .+$").Groups[1].Value);
                        }
                        if (log.Message.Contains("/print"))
                        {
                            Console.WriteLine(log.Combined);
                            ChatLogCommandPrintTrain?.Invoke(this,
                                Regex.Match(log.Combined, "^003C:.+/print (.+?) .+$").Groups[1].Value);
                        }
                        if (log.Message.Contains("/delete"))
                        {
                            Console.WriteLine(log.Combined);
                            ChatLogCommandDeleteTrain?.Invoke(this,
                                Regex.Match(log.Combined, "^003C:.+/delete (.+?) .+$").Groups[1].Value); 
                            //^003C:The command /delete (.*?) does not exist.$ => this works for multi word names, but only eng
                            //the currently used regex should work for all languages as it only searches for the eng /train /print /delete command 
                            //and then the next word following is the 'name'.
                            //better to just create different regex strings and use based on const gameLanguage string?
                        }
                        if (log.Message.Contains("/change"))
                        {
                            Console.WriteLine(log.Combined);
                            ChatLogCommandChangeTrain?.Invoke(this,
                                Regex.Match(log.Combined, "^003C:.+/change (.+?) .+$").Groups[1].Value); 
                        }
                        if (log.Message.Contains("/stop"))
                        {
                            Console.WriteLine(log.Combined);
                            ChatLogCommandStopRecordingTrain?.Invoke(this,
                                EventArgs.Empty); 
                        }
                    }
                }

                //if chatlog combined == a command , invoke chatLogCommandEvent with the command string
            }


        }
        
        
        public event EventHandler<string> ChatLogEvent;
        public event EventHandler<string> ChatLogCommandCreateTrain;
        public event EventHandler<string> ChatLogCommandPrintTrain;
        public event EventHandler<string> ChatLogCommandDeleteTrain;
        public event EventHandler<string> ChatLogCommandChangeTrain;
        public event EventHandler ChatLogCommandStopRecordingTrain;

    }
}
