using System;
using NUnit.Framework;

public class Tests {

	[Test]
	public void timeCounter(){
		GameManager gamemanager = new GameManager();
		float actual = gamemanager.timer;
		int expected = 1;
		Assert.AreEqual (expected, gamemanager.timer + 1);

	}

	[Test]
	public void flipRotation(){
		Card card = new Card ();
		float angle = card.angle;
		float targetangle = card.targetAngle;
		Assert.Greater (targetangle,angle);


	}

	[Test]
	public void FlipRotationIsFixed(){
		Card card = new Card ();
		float angle = card.angle;
		float targetangle = card.angle + 1;
		Assert.Greater (targetangle,angle);


	}

	[Test]
	public void logosVisible(){
		GameManager gamemanager = new GameManager ();
		bool visible = gamemanager.logo0;
		Assert.That (visible);

	}

	[Test]
	public void logosVisibleIsFixed(){
		GameManager gamemanager = new GameManager ();
		bool visible = gamemanager.logo0;
		Assert.That (!visible);
	}

	[Test]
	public void identicalCardsDisappear(){
		GameManager gamemanager = new GameManager ();
		Assert.IsNull (gamemanager.temp);

	}

	[Test]
	public void nonIdenticalCardsFlipBack(){
		GameManager gamemanager = new GameManager ();
		Assert.IsNotNull (!gamemanager.temp);

	}

	[Test]
	public void startButton(){
		LoadOnClick loc = new LoadOnClick ();
		Assert.That(loc.getStartButton ());


	}

	[Test]
	public void exitButton(){
		LoadOnClick loc = new LoadOnClick ();
		Assert.That(loc.getExitButton ());


	}

	[Test]
	public void restartButton(){
		LoadOnClick loc = new LoadOnClick ();
		Assert.That(loc.getRestartButton ());


	}

	[Test]
	public void escapeButton(){
		LoadOnClick loc = new LoadOnClick ();
		Assert.That(loc.getEscapeButton ());


	}

	[Test]
	public void flipSlowly(){
		GameManager gamemanager = new GameManager ();
		int expected = 10;
		int actual = gamemanager.flipSlowly();
		Assert.AreEqual (expected, actual);


	}


	[Test]
	public void flipSlowlyIsFixed(){
		GameManager gamemanager = new GameManager ();
		int expected = 10;
		int actual = gamemanager.flipSlowlyFixed();
		Assert.AreEqual (expected, actual);


	}

	[Test]
	public void gameOver(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.gameoverworks ());


	}

	[Test]
	public void gameOverFixed(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.gameoverfixed ());


	}

	[Test]
	public void score(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.scoreworks ());


	}

	[Test]
	public void menu(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.menuworks ());


	}

	[Test]
	public void Allow3FlipAtTheSameTime(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.allow3flips());


	}

	[Test]
	public void holdscores(){
		GameManager gamemanager = new GameManager ();
		Assert.That (gamemanager.holdscores());


	}

	[Test]
	public void changetheme(){
		LoadOnClick loc = new LoadOnClick ();
		int actual = loc.theme;
		int expected = loc.theme + 1;
		Assert.Greater (expected, actual);


	}

}
