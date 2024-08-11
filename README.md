# EventBus

Just an example project for usage of EventBus with C#. 

There is a class named EventBus. This class is singleton at the same time with inherited class. 

Class is containing two primary functions; Subscribe() and Publish(). 

Subscribe function is responsible for subscribing functions to events on channels.
Publish function is just triggering event with channel param. 

Channels are basicly category or group for events. They are categorizing virtualy events and they are 
representing way to triggering events.