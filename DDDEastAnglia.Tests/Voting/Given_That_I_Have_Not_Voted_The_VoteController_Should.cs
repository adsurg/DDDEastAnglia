﻿using DDDEastAnglia.DataAccess.Commands.Vote;
using NSubstitute;
using NUnit.Framework;

namespace DDDEastAnglia.Tests.Voting
{
    [TestFixture]
    public class Given_That_I_Have_Not_Voted_The_VoteController_Should : VotingTestBase
    {
        private const int KnownSessionId = 1;
        private const int UnknownSessionId = 10;

        [Test]
        public void Register_A_Vote_For_A_Session()
        {
           Controller.RegisterVote(KnownSessionId);
           MessageBus.Received().Send(Arg.Is<RegisterVoteCommand>(command => command.SessionId == KnownSessionId));
        }

        [Test]
        public void Set_A_Cookie_When_Trying_To_Remove_A_Session()
        {
            Controller.RemoveVote(KnownSessionId);
            ControllerInformationProvider.Received().SaveVotingCookie(Arg.Is<VotingCookie>(cookie => cookie.Id == CookieId));
        }

        [Test]
        public void Set_An_Empty_Cookie_When_Trying_To_Add_An_Unknown_Session()
        {
            Controller.RegisterVote(UnknownSessionId);
            ControllerInformationProvider.Received().SaveVotingCookie(Arg.Is<VotingCookie>(cookie => cookie.Id == CookieId));
        }

        [Test]
        public void Save_The_Vote_To_The_Database()
        {
            Controller.RegisterVote(KnownSessionId);
            MessageBus.Received().Send(Arg.Is<RegisterVoteCommand>(command => command.SessionId == KnownSessionId));
        }
    }
}
