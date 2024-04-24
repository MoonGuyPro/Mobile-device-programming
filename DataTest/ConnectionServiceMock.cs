using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;
using System.CodeDom.Compiler;
using ClientData;
using Newtonsoft.Json;

namespace ClientDataTest
{
    internal class ConnectionServiceMock : IConnectionService
    {
        public event Action<string>? Logger;
        public event Action? OnConnectionStateChanged;
        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;

        public Task Connect(Uri peerUri)
        {
            throw new NotImplementedException();
        }

        public Task Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            return true;
        }


        // Fields and methods for test purposes

        private Serializer serializer = Serializer.Create();
        public int lastId;

        public async Task SendAsync(string message)
        {
            if (serializer.GetResponseHeader(message) == ServerApiMock.GetCandidatesCommandHeader)
            {
                UpdateAllResponceMock responce = new UpdateAllResponceMock();
                responce.Header = ServerApiMock.UpdateAllResponceHeader;
                responce.Candidates.Add(new CandidateDTOMock { Id = 1, Name = "Andrzej" });
                OnMessage?.Invoke(serializer.Serialize(responce));
            }
            else if(serializer.GetResponseHeader(message) == ServerApiMock.VoteForCandidateCommandHeader)
            {
                VoteForCandidateCommandMock voteForCandidateCommandMock = serializer.Deserialize<VoteForCandidateCommandMock>(message);
                lastId = voteForCandidateCommandMock.CandidateId;

                VotingResponceMock votingResponceMock = new VotingResponceMock();
                votingResponceMock.Header = ServerApiMock.VotingResponceHeader;
                OnMessage?.Invoke(serializer.Serialize(votingResponceMock));
            }

            await Task.Delay(0);
        }

        public void MockUpdateAll(System.Collections.Generic.ICollection<CandidateDTOMock> candidates)
        {
            UpdateAllResponceMock responce = new UpdateAllResponceMock();
            responce.Header = ServerApiMock.UpdateAllResponceHeader;
            responce.Candidates = candidates;
            OnMessage?.Invoke(serializer.Serialize(responce));
        }

/*        public void MockVotingReminderChanged(int daysToElection)
        {
            VotingReminderMock reminder = new VotingReminderMock();
            reminder.Header = ServerApiMock.VotingResponceHeader;
            reminder.DaysToElection = daysToElection;
        }*/

        internal static class ServerApiMock
        {
            public static readonly string GetCandidatesCommandHeader = "GetCandidates";
            public static readonly string UpdateAllResponceHeader = "UpdateAllCandidates";
            public static readonly string VoteForCandidateCommandHeader = "VoteForCandidate";
            public static readonly string VotingResponceHeader = "VotingResponce";
            public static readonly string VoringReminderHeader = "VoringReminder";
        }


        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal abstract class ServerCommandMock
        {
            [Newtonsoft.Json.JsonProperty("Header", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string Header { get; set; }

        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal partial class GetCandidatesCommandMock : ServerCommandMock
        {

        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        public class CandidateDTOMock
        {
            [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int Id { get; set; }

            [Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string Name { get; set; }

            [Newtonsoft.Json.JsonProperty("Votes", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int Votes { get; set; }


        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal abstract class ServerResponseMock
        {
            [Newtonsoft.Json.JsonProperty("Header", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string Header { get; set; }


        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal class UpdateAllResponceMock : ServerResponseMock
        {
            [Newtonsoft.Json.JsonProperty("candidates", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public System.Collections.Generic.ICollection<CandidateDTOMock> Candidates { get; set; }


        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal class VoteForCandidateCommandMock : ServerCommandMock
        {
            [Newtonsoft.Json.JsonProperty("CandidateId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int CandidateId { get; set; }


        }

        [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal class VotingResponceMock : ServerResponseMock
        {
            [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int Id { get; set; }
        }

        [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
        internal class VotingReminderMock : ServerResponseMock
        {
            [Newtonsoft.Json.JsonProperty("daysToElection", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public int DaysToElection { get; set; }


        }
    }
}
