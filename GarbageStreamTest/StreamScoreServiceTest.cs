using GarbageStreamAPI.Exceptions;
using GarbageStreamAPI.Service;
using System;
using Xunit;

namespace GarbageStreamTest
{
    public class StreamScoreServiceTest
    {
        [Fact]
        public void TestScore()
        {
            StreamScoreService s = new StreamScoreService();

            Assert.Equal(1, s.Score("{}"));
            Assert.Equal(6, s.Score("{{{}}}"));
            Assert.Equal(5, s.Score("{{},{}}"));
            Assert.Equal(16, s.Score("{{{},{},{{}}}}"));
            Assert.Equal(1, s.Score("{<a>,<a>,<a>,<a>}"));
            Assert.Equal(9, s.Score("{{<ab>},{<ab>},{<ab>},{<ab>}}"));
            Assert.Equal(9, s.Score("{{<!!>},{<!!>},{<!!>},{<!!>}}"));
            Assert.Equal(3, s.Score("{{<a!>},{<a!>},{<a!>},{<ab>}}"));

            Assert.Throws<StreamScoreException>(() => s.Score(null));
            Assert.Throws<StreamScoreException>(() => s.Score(""));
            Assert.Throws<StreamScoreException>(() => s.Score("{"));
            Assert.Throws<StreamScoreException>(() => s.Score("{!}"));
            Assert.Throws<StreamScoreException>(() => s.Score("{}{}"));
            Assert.Throws<StreamScoreException>(() => s.Score("{<>{}}"));
            Assert.Throws<StreamScoreException>(() => s.Score("{<!>,{}}"));
            Assert.Throws<StreamScoreException>(() => s.Score("{{},}"));
        }
    }
}
