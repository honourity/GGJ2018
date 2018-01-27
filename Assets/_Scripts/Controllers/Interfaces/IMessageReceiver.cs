using UnityEngine;

public interface IMessageReceiver
{
   void ProcessMessage();
   Transform GetSignalTarget();
}