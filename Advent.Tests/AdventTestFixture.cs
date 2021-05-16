using System.Collections.Generic;
using System.IO;
using System.Text;
using Adventure.Net;
using ColossalCave;
using NUnit.Framework;

namespace Advent.Tests
{
    [TestFixture]
    public class AdventTestFixture 
    {
        protected readonly Parser parser = new Parser();

        protected virtual void OnSetUp() {}
        protected virtual void OnTearDown() {}
        
        [SetUp]
        public void SetUp()
        {
            Context.Story = new ColossalCaveStory();
            Output.Initialize(new StringWriter(new StringBuilder()));
            CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
            Context.Story.Initialize();
            Inventory.Clear();
            OnSetUp();
        }
 
        [TearDown]
        public void TearDown()
        {
            OnTearDown();
        }

        public Room Location
        {
            get { return Context.Story.Location; }
            set { Context.Story.Location = value; }
        }

        public Inventory Inventory
        {
            get { return Inventory; }
        }

        public Room Room<T>()
        {
            return Rooms.Get<T>();
        }



    }
}