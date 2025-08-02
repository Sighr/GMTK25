using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "SO/Custom Event", order = 0)]
public class CustomEvent : ScriptableObject
{
	public enum EventType
	{
		DayEnded,
		EscapeThroughDoor,
		EscapeThroughWindow
	}
	
	[SerializeField] private EventType eventType;
	private List<ICustomEventHandler> subscribers = new();

	public void Subscribe(ICustomEventHandler handler)
	{
		subscribers.Add(handler);
	}

	public void Unsubscribe(ICustomEventHandler handler)
	{
		subscribers.Remove(handler);
	}

	public void Fire()
	{
		var subscribersLocalCopy = new List<ICustomEventHandler>(subscribers);
		foreach (var subscriber in subscribersLocalCopy)
		{
			subscriber.HandleEvent(eventType);
		}
	}
}