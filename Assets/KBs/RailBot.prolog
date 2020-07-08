:- consult("UnityLogic/KBs/UnityLogicAgentAPI.prolog").


/* this desire(take_box(B)) is activated through the Drone. The RailBot picks up the Box and puts it back, if the box belongs to the same area.*/

add take_box(B) && (\+ belief jobDone(B)) => [
	check_artifact_belief(B, d_dir(D_Dir)),
	check_belief(direction(MyDirection)),
	D_Dir = MyDirection,
	act (getArea(D_Dir), Area),
	cr goto(Area),
	act pickUp(B),
	act (getDrone, Drone),
	(
		not(check_agent_belief(Drone, isBusy)),
		add_agent_belief(Drone, isBusy)
	),
	add_agent_desire(Drone, deliver_box(B)),
	act dropDown(Area),
	act (getChargingStation, Home),
	cr goto(Home),
	add_belief(jobDone(B)),

	stop
].


/* The RailBot takes the box and puts it on the exchange area*/

add take_box(B) && (\+ belief jobDone(B)) => [
	check_artifact_belief(B, d_dir(D_Dir)),
	check_belief(direction(MyDirection)),
	D_Dir \= MyDirection,
	act (getArea(D_Dir), Area),
	cr goto(Area),
	act pickUp(B),
	act (getExchangeArea, ExchangeArea),
	cr goto(ExchangeArea),
	act dropDown(ExchangeArea),
	act (getSortingBot, SortingBot),
	add_agent_desire(SortingBot, take_box(B)),
	act (getChargingStation, Home),
	cr goto(Home),
	add_belief(jobDone(B)),

	stop
].


/* The RailBot takes the box and puts it on the corrcts platform area and calls the Drone*/

add deliver_box(B) && (\+ belief jobDone(B)) => [
	act pickUp(B),
	check_artifact_belief(B, s_dir(S_Dir)),
	act (getArea(S_Dir), Area),
	cr goto(Area),
	act (getDrone, Drone),
	(
		not(check_agent_belief(Drone, isBusy)),
		add_agent_belief(Drone, isBusy)
	),
	add_agent_desire(Drone, deliver_box(B)),
	act dropDown(Area),
	act (getChargingStation, Home),
	cr goto(Home),
	add_belief(jobDone(B)),

	stop
].


