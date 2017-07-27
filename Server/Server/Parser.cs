using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Networking
{
    public class Parser
    {
        public Parser()
        {
        }

        public bool SendObject(BinaryWriter writer, object message) // returns connected status
        {

            var serializable = new Message();

            var @switch = new Dictionary<Type, Action> {
                { typeof(AuthenticationRequest), () => {
                        serializable.authenticationRequest = (AuthenticationRequest)message;
                    } },
                { typeof(AuthenticationResponse), () => {
                        serializable.authenticationResponse = (AuthenticationResponse)message;
                    } },
                { typeof(GameInit), () => {
                        serializable.gameInit = (GameInit)message;
                    } },
                { typeof(Move), () => {
                        serializable.move = (Move)message;
                    } },
                { typeof(TurnChange), () => {
                        serializable.turnChange = (TurnChange)message;
                    } },
                { typeof(Status), () => {
                        serializable.status = (Status)message;
                    } },
                { typeof(SendCards), () => {
                        serializable.sendCards = (SendCards)message;
                    } },
                { typeof(UseCard), () => {
                        serializable.useCard = (UseCard)message;
                    } }
            };

            @switch[message.GetType()]();

            string json = JsonConvert.SerializeObject(serializable);

            try
            {
                writer.Write(json);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public object RecvObject(BinaryReader reader)
        { // returns null if failed
            string json = "{}";

            try
            {
                json = reader.ReadString();
            }
            catch
            {
                // reading string failed
            }

            try
            {
                var deserialised = JsonConvert.DeserializeObject<Message>(json);

                if (deserialised.authenticationRequest != null)
                { // only one of the Message.variable can be non null in one message
                    return (AuthenticationRequest)deserialised.authenticationRequest;
                }
                else if (deserialised.authenticationResponse != null)
                {
                    return (AuthenticationResponse)deserialised.authenticationResponse;
                }
                else if (deserialised.gameInit != null)
                {
                    return (GameInit)deserialised.gameInit;
                }
                else if (deserialised.move != null)
                {
                    return (Move)deserialised.move;
                }
                else if (deserialised.turnChange != null)
                {
                    return (TurnChange)deserialised.turnChange;
                }
                else if (deserialised.status != Status.None)
                { // be careful with enums in Message
                    return (Status)deserialised.status;
                }
                else if (deserialised.sendCards != null)
                { // be careful with enums in Message
                    return (SendCards)deserialised.sendCards;
                }
                else if (deserialised.useCard != null)
                { // be careful with enums in Message
                    return (UseCard)deserialised.useCard;
                }
                else {
                    // parsing message failed
                    return null;
                }
            }
            catch
            {
                // deserialisation failed
                return null;
            }
        }
    }
}
