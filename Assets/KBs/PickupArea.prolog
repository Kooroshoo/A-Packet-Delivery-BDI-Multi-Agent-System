:- consult("UnityLogic/KBs/UnityLogicAgentAPI.prolog").


/* this desire(call_drone(B)) is activated trough GameManeger. The PickupArea calls the Drone*/

add call_drone(B) && true => [
	act (getDrone, Drone),
	(
		not(check_agent_belief(Drone, isBusy)),
		add_agent_belief(Drone, isBusy)
	),
	add_agent_desire(Drone,take_box(B)),
	
	stop
].


/* destroys the Box*/

add pickup(B) && true => [
	act destroy(B),

	stop
].