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
        protected readonly Library L = new Library();

        protected virtual void OnSetUp() {}
        protected virtual void OnTearDown() {}
        
        [SetUp]
        public void SetUp()
        {
            Context.Story = new ColossalCaveStory();
            Context.Output = new Output(new StringWriter(new StringBuilder()));
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