:- consult("UnityLogic/KBs/UnityLogicAgentAPI.prolog").


/* this desire(take_box(B) is activated trough PickupArea, and the Drone comes and picks up the Box and puts it on the correct platform and calls the RailBot*/

add take_box(B) && (true) => [
	cr takeOff,
	cr goto(B),
	cr land,
	act pickup(B),
	cr takeOff,
	check_artifact_belief(B, s_dir(S_Dir)),
	check_artifact_belief(B, d_dir(D_Dir)),
	act (getLandingZone(S_Dir, D_Dir), LandingZone),
	cr takeOff,
	cr goto(LandingZone),
	cr land,
	act dropDown,
	act (getRailBot(S_Dir), RailBot),
	add_agent_desire(RailBot, take_box(B)),
	add_agent_belief(RailBot, direction(S_Dir)),
	cr takeOff,
	add_belief(jobDone(B)),
	act (getChargingStation, Home),
	cr goto(Home),
	cr land,
	del_belief(isBusy),

	stop
].


/* The Drone delivers the box from the RailBot to the PickupArea*/

add deliver_box(B) && (true) => [
	check_artifact_belief(B, destination(D)),
	cr takeOff,
	cr goto(B),
	cr land,
	act pickup(B),
	cr takeOff,
	cr goto(D),
	cr land,
	cr dropdown,
	cr takeOff,
	add_desire(go_home),
	add_agent_desire(D, pickup(B)),
	add_belief(jobDone(B)),
	act (getChargingStation, Home),
	cr goto(Home),
	cr land,
	del_belief(isBusy),

	stop
].


