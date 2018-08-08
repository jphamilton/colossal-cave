using System.IO;
using System.Text;
using ColossalCave;
using ColossalCave.MyObjects;
using Adventure.Net;
using Adventure.Net.Verbs;
using NUnit.Framework;
using ColossalCave.MyRooms;

namespace Advent.Tests
{
    [TestFixture]
    public class UserInputTests
    {
        private Library L = new Library();
        private Parser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new Parser();
            // initialize story and put player in "Inside Building"
            Context.Story = new ColossalCaveStory();
            Context.Output = new Output(new StringWriter(new StringBuilder()));
            Context.CommandPrompt = new CommandPrompt(new StringWriter(), new StringReader(""));
            Context.Story.Initialize();
            Context.Story.Location = Rooms.Get<InsideBuilding>();
            Inventory.Clear();
        }

        [Test]
        public void all_with_exceptional_grammar()
        {
            var input = new UserInput();
            var result = input.Parse("take all except bottle");
            Assert.AreEqual("all", result.Pregrammar);
            var bottle = Objects.Get<Bottle>();
            Assert.IsTrue(result.Objects.Count > 0);
            // even objects that can't be taken should be in the input results
            Assert.IsTrue(result.Objects.Contains(Objects.Get<SewerPipes>()));
            Assert.IsTrue(result.Exceptions.Contains(bottle));
        }

        [Test]
        public void all_with_multiple_exceptional_objects_grammar()
        {
            var input = new UserInput();
            var result = input.Parse("take all except bottle and keys");
            Assert.AreEqual("all", result.Pregrammar);
            var take = VerbList.GetVerbByName("take");
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            Assert.AreSame(take, result.Verb);
            Assert.IsTrue(result.Objects.Count > 0);
            Assert.IsTrue(result.Exceptions.Contains(bottle));
            Assert.IsTrue(result.Exceptions.Contains(keys));
        }

        [Test]
        public void object_only()
        {
            var input = new UserInput();
            var result = input.Parse("take bottle");
            Assert.AreEqual("x", result.Pregrammar);
        }

        [Test]
        public void object_and_indirect_object()
        {
            var input = new UserInput();
            var result = input.Parse("rub lamp with food");
            Assert.AreEqual("x with y", result.Pregrammar);
        }

        [Test]
        public void ending_with_preposition()
        {
            var input = new UserInput();
            var result = input.Parse("turn lamp on");
            Assert.AreEqual("x on", result.Pregrammar);
        }

        [Test]
        public void should_not_understand()
        {
            var results = parser.Parse("");
            //var input = new UserInput();
            //var result = input.Parse("");
            Assert.AreEqual(L.DoNotUnderstand, results[0]);
        }

        [Test]
        public void should_not_recognize_verb()
        {
            var results = parser.Parse("snark");
//            var input = new UserInput();
            //var result = input.Parse("snark");
            Assert.AreEqual(L.VerbNotRecognized, results[0]);
        }

        [Test]
        public void multi_preposition_grammars()
        {
            var input = new UserInput();
            var result = input.Parse("put keys on bottle");
            var put = VerbList.GetVerbByName("put");
            Assert.AreEqual(put, result.Verb);
        }

        [Test]
        public void different_verbs_with_same_name()
        {
            var input = new UserInput();
            var result = input.Parse("switch on lamp");
            Assert.AreEqual(typeof(SwitchOn), result.Verb.GetType());
            result = input.Parse("switch lamp on");
            Assert.AreEqual(typeof(SwitchOn), result.Verb.GetType());
            result = input.Parse("switch off lamp");
            Assert.AreEqual(typeof(SwitchOff), result.Verb.GetType());
            result = input.Parse("switch lamp off");
            Assert.AreEqual(typeof(SwitchOff), result.Verb.GetType());
        }

        [Test]
        public void object_not_present()
        {
            var results = parser.Parse("take cage");

            //var input = new UserInput();
            // this object is in a different room
            //var result = input.Parse("take cage");
            Assert.AreEqual(L.CantSeeObject, results[0]);
            // object is here, indirect object is not
            results = parser.Parse("put bottle in cage");
            Assert.AreEqual(L.CantSeeObject, results[0]);
            // object is not here, indirect object is here
            results = parser.Parse("put batteries in lamp");
            Assert.AreEqual(L.CantSeeObject, results[0]);
        }

        [Test]
        public void directions()
        {
            var input = new UserInput();
            var result = input.Parse("go north");
            Assert.IsTrue(result.Verb is North);

            result = input.Parse("south");
            Assert.IsTrue(result.Verb is South);
        }

        [Test]
        public void bad_grammar_good_words()
        {
            // don't try to parse nonsense
            var results = parser.Parse("take south bottle");
            Assert.AreEqual(L.DoNotUnderstand, results[0]);

            results = parser.Parse("take drop bottle");
            Assert.AreEqual(L.CantSeeObject, results[0]);

            results = parser.Parse("bottle drop");
            Assert.AreEqual(L.VerbNotRecognized, results[0]);

            results = parser.Parse("take bottle drop");
            Assert.AreEqual("I only understood you as far as wanting to take the small bottle.", results[0]);

        }
    }
}
