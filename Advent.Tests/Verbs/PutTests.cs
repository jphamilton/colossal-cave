﻿using System;
using Adventure.Net;
using ColossalCave.Objects;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class PutTests : AdventTestFixture
    {
        [Test]
        public void what_do_you_want_to_put_the_bottle_in()
        {
            CommandPrompt.FakeInput("cage");

            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);
               
            var results = parser.Parse("put bird");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
        }

        [Test]
        public void restate_command_after_incomplete_question()
        {
            CommandPrompt.FakeInput("put bird in cage");

            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);

            var results = parser.Parse("put bird");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
        }

        [Test]
        public void what_do_you_want_to_put_the_bottle_on()
        {
            CommandPrompt.FakeInput("keys");
            
            var bottle = Items.Get<Bottle>();
            Location.Objects.Add(bottle);

            var keys = Items.Get<SetOfKeys>();
            Location.Objects.Add(keys);

            var results = parser.Parse("put bottle on");
            Assert.AreEqual("You need to be holding the small bottle before you can put it on top of something else.", results[0]);
        }

        [Test]
        public void just_put_object_not_present()
        {
            CommandPrompt.FakeInput("bird in cage");

            var results = parser.Parse("put");
            Assert.AreEqual("You can't see any such thing.", results[0]);
        }

        [Test]
        public void just_put_object_present()
        {
            CommandPrompt.FakeInput("bird in cage");

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("put");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
        }

        [Test]
        public void start_with_bird()
        {
            CommandPrompt.FakeInput("bird\ncage");

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("put");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]); 
        }

        [Test]
        public void start_with_bird_should_actually_do_implicit_put()
        {
            // bird has a Before<Insert> action defined so the parser should implicitly call it:
            //
            // > put
            //   What do you want to put?
            // > bird
            //   (in the little bird)
            //   Don't put the poor bird in the little bird!
            //
            // However, the Before<Insert> action on the bird should be rewritten to disallow this specific case.

            CommandPrompt.FakeInput("bird");

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Items.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("put");
            
            Assert.AreEqual("(in the little bird)", results[0]);
            Assert.AreEqual("Don't put the poor bird in the little bird!", results[1]);
        }

        [Test]
        public void what_do_you_want_to_put_the_bird_in()
        {
            // inventory = bird in cage
            // put
            // what do you want put?
            // bird
            // what do you want to put the little bird in?
            // cage
            // You already have the little bird. If you take it out of the cage it will likely fly away from you.

            CommandPrompt.FakeInput("bird\ncage");

            var cage = Items.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Items.Get<LittleBird>();
            cage.Add(bird);

            var results = parser.Parse("put");
            Assert.AreEqual("You already have the little bird.", results[0]);
            Assert.AreEqual("If you take it out of the cage it will likely fly away from you.", results[1]);
        }


        [Test]
        public void just_put_all()
        {
            // need to look at inform source. where does "those things in" come from?
            var results = parser.Parse("put all");
            Assert.AreEqual("What do you want to put those things in?", results[0]);
        }

        [Test]
        public void just_put_all_except()
        {
            CommandPrompt.FakeInput("bird");

            var results = parser.Parse("put all except");
            Assert.AreEqual(1, results.Count);

            var output = Output.Buffer.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual("What do you want to put?", output[0]); // bird
            Assert.AreEqual("You can't see any such thing.", output[1]);
            //Assert.AreEqual("", output[2]);

            // Do I need to pass the results up to the first parse?????
            //Assert.AreEqual("You can't see any such thing.", results[0]);
        }

        [Test]
        public void i_dont_understand_that_sentence()
        {
            var bottle = Items.Get<Bottle>();
            Location.Objects.Add(bottle);
            var lantern = Items.Get<BrassLantern>();
            Location.Objects.Add(lantern);

            var results = parser.Parse("put bottle lantern");
            Assert.AreEqual("I didn't understand that sentence.", results[0]);
        }

        [Test]
        public void can_put_down_object()
        {

        }
    }
}