﻿//using Adventure.Net.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Adventure.Net
//{

//    public class Parser : IParser
//    {
//        private List<ParserResult> results;
        
//        private State currentState;

//        private enum State
//        {
//            Before,
//            During,
//            After
//        }
        
//        private InputResult inputResult;
//        private Item objectInPlay;

//        public void Print(string msg)
//        {
//            IList<string> messages = GetMessageList();

//            if (inputResult.IsAll || inputResult.Objects.Count > 1)
//            {
//                if (inputResult.Objects.Count > 1)
//                    messages.Add(objectInPlay.Name + ": " + msg);
//                else if (inputResult.Objects.Count == 1)
//                {
//                    messages.Add("(the " + objectInPlay.Name + ")");
//                    messages.Add(msg);
//                }
//            }
//            else
//            {
//                messages.Add(msg);
//            }
//        }

//        public void Print(string format, params object[] arg)
//        {
//            string msg = string.Format(format, arg);
//            Print(msg);
//        }

//        private IList<string> GetMessageList()
//        {
//            if (currentState == State.Before)
//                return results.Last().BeforeMessages;
//            if (currentState == State.After)
//                return results.Last().AfterMessages;
//            return results.Last().DuringMessages;
//        }

//        public IList<string> Parse(string input)
//        {
//            return Parse(input, true);
//        }

//        private IList<string> Parse(string input, bool showOutput)
//        {
//            //Context.Parser = this;
//            //Context.Item = null;
//            //Context.IndirectItem = null;
            
//            results = new List<ParserResult>();

//            bool wasLit = CurrentRoom.IsLit();

//            var userInput = new UserInput();

//            if (inputResult != null && inputResult.IsAskingQuestion)
//            {
//                var tokenizer = new InputTokenizer();
//                var tokens = tokenizer.Tokenize(input);

//                if (!tokens.StartsWithVerb())
//                {
//                    string temp = null;
//                    if (inputResult.Verb.IsNull == false)
//                        temp += inputResult.Verb.Name + " ";
//                    if (inputResult.Objects.Count > 0)
//                        temp += inputResult.Objects[0].Synonyms[0] + " ";
//                    if (!string.IsNullOrEmpty(inputResult.Preposition))
//                        temp += inputResult.Preposition + " ";
//                    if (inputResult.IndirectObject != null)
//                        temp += inputResult.IndirectObject.Name + " ";
//                    if (temp != null)
//                        input = temp + input;
//                }

//            }

//            inputResult = userInput.Parse(input);

//            if (!inputResult.Handled)
//            {
//                HandleInputResult();
//            }

//            if (!wasLit && CurrentRoom.IsLit())
//            {
//                CurrentRoom.Look(true);
//            }

//            return GetResults(showOutput, inputResult.ParserResults);
//        }

//        private IList<string> GetResults(bool showOutput, IList<string> additionalResults = null)
//        {
//            var list = new List<string>();
//            if (additionalResults != null)
//            {
//                list.AddRange(additionalResults);
//            }

//            Action<string> print = (msg) =>
//                {
//                    string[] lines = msg.Split('\n');
//                    foreach (var line in lines)
//                    {
//                        list.Add(line);
//                        if (showOutput)
//                        {
//                            Output.Print(line);
//                        }
//                    }
//                };

//            foreach (var result in results)
//            {
//                foreach (var msg in result.BeforeMessages)
//                {
//                    print(msg);
//                }

//                foreach (var msg in result.DuringMessages)
//                {
//                    print(msg);
//                }

//                foreach (var msg in result.AfterMessages)
//                {
//                    print(msg);
//                }
//            }

//            return list;
//        }

//        private void HandleInputResult()
//        {
//            var builder = new CommandBuilder(inputResult);
//            var commands = builder.Build();

//            foreach(var command in commands)
//            {
//                ExecuteCommand(command);
//            }
            
//        }

//        public bool ExecuteCommand(Command command)
//        {
//            Context.Item = command.Object;
//            Context.IndirectItem = command.IndirectObject;

//            var parserResult = new ParserResult();
//            results.Add(parserResult);

//            // after, before, and during needs to be modified to return the object
//            // that handled the message

//            bool result = Before(command);

//            if (!result)
//            {
//                result = During(command);
//                if (result)
//                {
//                    After(command);
//                }
//            }

//            return result;
//        }

//        private bool Before(Command command)
//        {
//            currentState = State.Before;
//            return Before(command, command.IndirectObject) || (Before(command, command.Object) || Before(command, Context.Story.Location));
//        }

//        private bool Before(Command command, Item obj)
//        {
//            if (obj != null)
//            {
//                objectInPlay = obj;
//                var before = obj.Before(command.Verb.GetType());
//                if (before != null)
//                {
//                    return before();
//                }
//            }

//            return false;
//        }

//        private bool During(Command command)
//        {
//            currentState = State.During;
//            objectInPlay = command.Object;
//            return command.Action();
//        }

//        private bool After(Command command)
//        {
//            currentState = State.After;
//            return After(command, command.IndirectObject) || (After(command, command.Object) || After(command, Context.Story.Location));
//        }

//        private bool After(Command command, Item obj)
//        {
//            if (obj != null)
//            {
//                objectInPlay = obj;
//                var after = obj.After(command.Verb.GetType());
//                if (after != null)
//                {
//                    after();
//                }
//            }

//            return false;
//        }


//    }

//}



