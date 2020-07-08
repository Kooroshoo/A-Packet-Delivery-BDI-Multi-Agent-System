:- consult("UnityLogic/KBs/UnityLogicAgentAPI.prolog").

desire generateBoxes.

add generateBoxes && true =>
[

	add_desire(createBox(1,8)),
	add_desire(createBox(7,0)),
	add_desire(createBox(2,3)),
	add_desire(createBox(4,5)),
	add_desire(createBox(5,2)),
	add_desire(createBox(3,0)),

	add_desire(wait(15)),

	add_desire(createBox(7,8)),
	add_desire(createBox(8,4)),
	add_desire(createBox(3,2)),
	add_desire(createBox(1,0)),


	stop
].

/* Creating Box B, with all the necessary beliefs, and activating the call_drone(B) desire of the PickupArea*/

add createBox(StartIndex, DestIndex) && true =>
[
	act (getArea(StartIndex), S),
	act (getArea(DestIndex), D),
	act (spawnBox(S, D), B),
	add_artifact_belief(B, start(S)),
	add_artifact_belief(B, destination(D)),
	check_agent_belief(S, area(S_Dir)),
	check_agent_belief(D, area(D_Dir)),
	add_artifact_belief(B, s_dir(S_Dir)),
	add_artifact_belief(B, d_dir(D_Dir)),

	add_agent_desire(S, call_drone(B)),
	
	stop
].

add wait(Seconds) && true =>
[
	cr waitForSeconds(Seconds),
	stop
].