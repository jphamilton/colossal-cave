using Adventure.Net;
using Adventure.Net.Verbs;
using ColossalCave.Objects;
using ColossalCave.Places;
using System.Linq;
using Xunit;

namespace Tests.ParserTests
{
    public partial class CommandLineParserTests : BaseTestFixture
    {
        [Fact]
        public void should_not_understand()
        {
            var result = parser.Parse("");
            Assert.Equal(Messages.DoNotUnderstand, result.Error);
        }

        [Fact]
        public void should_not_recognize_verb()
        {
            var results = parser.Parse("snark");
            Assert.Equal(Messages.VerbNotRecognized, results.Error);
        }


        [Fact]
        public void object_is_present()
        {
            var result = parser.Parse("take bottle");
            var bottle = Objects.GetByName("bottle");

            Assert.True(result.Verb is Take);
            Assert.Contains(bottle, result.Objects);
        }

        [Fact]
        public void multiple_objects_specified()
        {
            var result = parser.Parse("take bottle, keys and lamp");
            
            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.Contains(Objects.GetByName("keys"), result.Objects);
            Assert.Contains(Objects.GetByName("lamp"), result.Objects);

        }

        [Fact]
        public void ending_with_preposition()
        {
            var result = parser.Parse("turn lamp on");

            Assert.Contains(Objects.Get<BrassLantern>(), result.Objects);
            Assert.True(result.Preposition is Preposition.On);
            Assert.True(result.Verb is Turn);
        }

        [Fact]
        public void object_not_present()
        {
            var result = parser.Parse("take cage");

            Assert.DoesNotContain(Objects.Get<WickerCage>(), result.Objects);
            Assert.Equal(result.Error, Messages.CantSeeObject);

        }

        [Fact]
        public void multiple_objects_present_but_one_is_not()
        {
            var result = parser.Parse("take bottle, keys and cage");

            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.Contains(Objects.GetByName("keys"), result.Objects);
            Assert.DoesNotContain(Objects.GetByName("cage"), result.Objects);
            Assert.Equal(Messages.PartialUnderstanding(result.Verb, result.Objects.First()), result.Error);
            
        }

        [Fact]
        public void object_indirect_object()
        {
            var result = parser.Parse("put bottle in lamp"); 
            Assert.Contains(Objects.Get<Bottle>(), result.Objects);
            Assert.True(result.Preposition is Preposition.In);
            Assert.Equal(Objects.Get<BrassLantern>(), result.IndirectObject);
        }

        [Fact]
        public void object_indirect_object_2()
        {
            var result = parser.Parse("rub lamp with food");

            Assert.Contains(Objects.Get<BrassLantern>(), result.Objects);
            Assert.Equal(Objects.Get<TastyFood>(), result.IndirectObject);
            Assert.True(result.Preposition is Preposition.With);
            Assert.True(result.Verb is Rub);
        }

        [Fact]
        public void handle_all()
        {
            var result = parser.Parse("take all");

            Assert.True(result.Verb is Take);
            Assert.True(result.Objects.Count > 0);
            Assert.Contains(Objects.GetByName("keys"), result.Objects);
            Assert.Contains(Objects.GetByName("food"), result.Objects);
            Assert.Contains(Objects.GetByName("lamp"), result.Objects);
            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.Contains(Objects.GetByName("stream"), result.Objects);
            Assert.Contains(Objects.GetByName("pipes"), result.Objects);
            Assert.Contains(Objects.GetByName("wellhouse"), result.Objects);
            Assert.Contains(Objects.GetByName("spring"), result.Objects);
            
            // should not pick up the room!
            Assert.DoesNotContain(Room<InsideBuilding>(), result.Objects);

        }

        [Fact]
        public void handle_except_one_object()
        {
            var result = parser.Parse("take all except keys");

            Assert.True(result.Verb is Take);
            Assert.True(result.Objects.Count > 0);
            Assert.DoesNotContain(Objects.GetByName("keys"), result.Objects);
        }

        [Fact]
        public void handle_except_two_objects()
        {
            var result = parser.Parse("take all except keys and bottle");

            Assert.True(result.Verb is Take);
            Assert.True(result.Objects.Count > 0);
            Assert.DoesNotContain(Objects.GetByName("keys"), result.Objects);
            Assert.DoesNotContain(Objects.GetByName("bottle"), result.Objects);
        }

        [Fact]
        public void handle_except_two_objects_but_one_not_preset()
        {
            // cage is not in the room
            var result = parser.Parse("take all except keys and cage");

            Assert.True(result.Verb is Take);
            Assert.Equal(Messages.CantSeeObject, result.Error);
        }

        [Fact]
        public void handle_all_with_bad_grammar()
        {
            // cage is not in the room
            var result = parser.Parse("take all donkey sauce");

            Assert.True(result.Verb is Take);
            Assert.Equal(Messages.CantSeeObject, result.Error);
        }

        [Fact]
        public void bad_grammar_good_words()
        {
            var result = parser.Parse("take drop bottle");
            Assert.Equal(Messages.CantSeeObject, result.Error);
        }

        [Fact]
        public void bad_grammar_good_words_2()
        {
            var result = parser.Parse("bottle drop");
            Assert.Equal(Messages.VerbNotRecognized, result.Error);
        }

        [Fact]
        public void should_resolve_to_directional_verb()
        {
            var result = parser.Parse("go west");

            Assert.True(result.Verb is West);
            Assert.Empty(result.Objects);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void should_also_resolve_to_directional_verb()
        {
            var result = parser.Parse("west");

            Assert.True(result.Verb is West);
            Assert.Empty(result.Objects);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void should_handle_preposition_that_matches_direction()
        {
            // this conflicts with "go east" which replaces go with east at the verb
            // down is a direction and a preposition
            var result = parser.Parse("put bottle down");

            Assert.True(result.Verb is Put);
            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.True(result.Preposition is Preposition.Down);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void should_handle_preposition_that_matches_direction_2()
        {
            // this conflicts with "go east" which replaces go with east at the verb
            // down is a direction and a preposition
            var result = parser.Parse("put down bottle");

            Assert.True(result.Verb is Put);
            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.True(result.Preposition is Preposition.Down);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void should_handle_multi_with_preposition_matching_direction()
        {
            var result = parser.Parse("put bottle, keys and lamp down");

            Assert.True(result.Verb is Put);
            Assert.Contains(Objects.GetByName("bottle"), result.Objects);
            Assert.Contains(Objects.GetByName("keys"), result.Objects);
            Assert.Contains(Objects.GetByName("lamp"), result.Objects);
            Assert.True(result.Preposition is Preposition.Down);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void multi_preposition_grammars()
        {
            var result = parser.Parse("put keys on bottle");
            
            Assert.Contains(Objects.Get<SetOfKeys>(), result.Objects);
            Assert.Equal(Objects.Get<Bottle>(), result.IndirectObject);
            Assert.True(result.Verb is Put);
        }

        [Fact]
        public void should_handle_all_except_with_preposition_matching_direction()
        {
            var result = parser.Parse("put all except keys down");

            Assert.True(result.Verb is Put);
            Assert.DoesNotContain(Objects.GetByName("keys"), result.Objects);
            Assert.True(result.Preposition is Preposition.Down);
            Assert.True(string.IsNullOrEmpty(result.Error));
        }

        [Fact]
        public void should_handle_multiple_objects_with_same_name()
        {
            var room = Context.Story.Location;
            
            var red = new RedHat();
            red.Initialize();

            var black = new BlackHat();
            black.Initialize();

            Objects.Add(red);
            Objects.Add(black);
            room.Objects.Add(red);
            room.Objects.Add(black);

            var result = parser.Parse("take hat");

            Assert.True(result.Verb is Take);
            
            // TODO: implement which do you mean
        }

        [Fact]
        public void should_handle_multiple_objects_with_same_name_both_not_present()
        {
            var red = new RedHat();
            red.Initialize();

            var black = new BlackHat();
            black.Initialize();

            Objects.Add(red);
            Objects.Add(black);

            var result = parser.Parse("take hat");

            Assert.True(result.Verb is Take);
            Assert.Equal(Messages.CantSeeObject, result.Error);

        }

        [Fact]
        public void room_as_object()
        {
            Context.Story.Location = Room<EndOfRoad>();
            var wellHouse = Objects.Get<WellHouse>();

            var result = parser.Parse("go house");

            Assert.True(result.Verb is Go);
            Assert.Contains(wellHouse, result.Objects);
        }

        [Fact]
        public void directions()
        {
            var result = parser.Parse("go north");
            Assert.True(result.Verb is North);

            result = parser.Parse("south");
            Assert.True(result.Verb is South);
        }

        [Fact]
        public void more_bad_grammar_good_words()
        {
            var result = parser.Parse("take bottle drop");
            Assert.True(result.Objects.Single() is Bottle);
            Assert.Equal(Messages.PartialUnderstanding(result.Verb, result.Objects.Single()), result.Error);
        }

        [Fact]
        public void should_handle_one_word_commands()
        {
            var result = parser.Parse("look");
            Assert.True(result.Verb is Look);
            Assert.True(!result.Objects.Any());
            Assert.Null(result.IndirectObject);
        }

        [Fact]
        public void cannot_use_multiple_objects_with_that_verb()
        {
            var result = parser.Parse("look at all except");
            Assert.Equal(Messages.MultiNotAllowed, result.Error);
        }


        [Fact]
        public void should_handle_multiheld()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();

            CurrentRoom.Objects.Remove(bottle);
            CurrentRoom.Objects.Remove(keys);

            Inventory.Add(bottle);
            Inventory.Add(keys);
            
            var result = parser.Parse("drop all");
            Assert.Contains(bottle, result.Objects);
            Assert.Contains(keys, result.Objects);
            Assert.True(result.Verb is Drop);
        }

        [Fact]
        public void should_handle_multiheld_not_preset()
        {
            // cage is not here
            var result = parser.Parse("drop cage");
            Assert.Equal(Messages.CantSeeObject, result.Error);
        }

        [Fact]
        public void can_distinguish_prepositions_from_directions()
        {
            Location = Room<OutsideGrate>();
            
            var result = parser.Parse("close up grate");
            
            Assert.Equal(Verb.Get<Close>(), result.Verb);
            Assert.True(result.Preposition is Preposition.Up);
            Assert.True(result.Objects.Single() is Grate);

        }

    }
}

