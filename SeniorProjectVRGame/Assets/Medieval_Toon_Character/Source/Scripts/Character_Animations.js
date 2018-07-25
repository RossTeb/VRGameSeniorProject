#pragma strict
internal var animator:Animator;
var v:float;
var h:float;
var run:float;
public var aniDelay:float;
var rocktime:float;
public var rock:GameObject;
public var hand:GameObject;
var canThrow:int;


function Start () {
	animator=GetComponent (Animator);
}

function Update () {
	v=Input.GetAxis("Vertical");
	h=Input.GetAxis("Horizontal");
    if (Input.GetKeyDown("f")&&canThrow ==0) {
        animator.SetBool("Rock", true);
        rocktime = Time.time+aniDelay;
        canThrow = 1;

    } else {
        animator.SetBool("Rock", false);
    }
	if (animator.GetFloat("Run")==0.2){
		if (Input.GetKeyDown("space")){
			animator.SetBool("Jump",true);
		}
	}
    if (Time.time >= rocktime && rocktime > 0) {
        Instantiate(rock, hand.transform.position, Quaternion.identity);
        canThrow = 0;
        rocktime = 0;
    }
	Sprinting();
	
}

function FixedUpdate (){
	animator.SetFloat("Walk",v);
	animator.SetFloat("Run",run);
	animator.SetFloat("Turn",h);
}

function Sprinting(){
	if (Input.GetKey(KeyCode.LeftShift)){
		run=0.2;
	}
	else
	{
		run=0.0;
	}
}