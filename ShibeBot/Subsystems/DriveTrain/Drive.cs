﻿using System.Media;
using ShibeBot.Commands;
using ShibeBot.OperatorInteface;
using CTRE;
using WPILib;
using WPILib.Commands;
using WPILib.SmartDashboard;
using System;

namespace ShibeBot.Subsystems.DriveTrain
{
	public class DriveTrain : Subsystem
	{

		private static readonly TalonSRX LeftPrimary = new TalonSRX(0);
		private static readonly TalonSRX RightPrimary = new TalonSRX(1);
		private static readonly TalonSRX LeftSecondary = new TalonSRX(2);
		private static readonly TalonSRX RightSecondary = new TalonSRX(3);

		private RobotDrive _drive = new RobotDrive(RightPrimary, LeftPrimary);

		protected override void InitDefaultCommand()
		{
			SetDefaultCommand(new DriveCommand());
		}

		private double leftX = 0;
		private double leftY = 0;
		private double rightX = 0;
		private double rightY = 0;

		public void TankDrive(Joystick stick)
		{
			AutoLerp(stick, 0.05);
			_drive.MaxOutput = 1 - stick.GetRawAxis(XboxMap.RightTrigger);
			_drive.TankDrive(-leftY, -rightY);
		}

		public void ArcadeDrive(Joystick stick)
		{
			AutoLerp(stick, 0.05);
			_drive.MaxOutput = 1 - stick.GetRawAxis(XboxMap.RightTrigger);
			_drive.ArcadeDrive(-leftY, -leftX);
		}

		public void DiveDistance(double distance)
		{
			_drive.DriveDistance(distance);
		}


		public void DriveManually(double leftSpeed, double rightSpeed)
		{
			_drive.TankDrive(leftSpeed, rightSpeed);
		}

		private void AutoLerp(Joystick stick, double amount)
		{
			leftX = Lerp(leftX, stick.GetRawAxis(XboxMap.LeftX), amount);
			leftY = Lerp(leftY, stick.GetRawAxis(XboxMap.LeftY), amount);
			rightX = Lerp(rightX, stick.GetRawAxis(XboxMap.RightX), amount);
			rightY = Lerp(rightY, stick.GetRawAxis(XboxMap.RightY), amount);
			SmartDashboard.PutNumber("Left X", leftX);
			SmartDashboard.PutNumber("Left Y", leftY);
			SmartDashboard.PutNumber("Right X", rightX);
			SmartDashboard.PutNumber("Right Y", rightY);
		}

		private double Lerp(double first, double second, double amount)
		{
			return (1 - amount) * first + amount * second;
		}
	}
}


//public class Drive
//{

//	private const int WheelDiameter = 4;
//	private const int InchesToFeet = 12;


//	private TalonSRX RightPrimary;
//	private TalonSRX LeftPrimary;
//	private TalonSRX RightSecondary;
//	private TalonSRX LeftSecondary;

//	public double MaxOutput = 1;

//	public Drive(TalonSRX _rightPrimary, TalonSRX _leftPrimary, TalonSRX _rightSecondary, TalonSRX _leftSecondary)
//	{
//		RightPrimary = _rightPrimary;
//		LeftPrimary = _leftPrimary;
//		LeftSecondary = _leftSecondary;
//		RightSecondary = _rightSecondary;
//	}

//	public void ArcadeDrive(double x, double y)
//	{
//		double left = (y + x) * MaxOutput;
//		double right = (y - x) * MaxOutput;

//		RightPrimary.Set(right);
//		RightSecondary.Set(right);

//		LeftPrimary.Set(left);
//		LeftSecondary.Set(left);

//	}

//	public void TankDrive(double y1, double y2)
//	{
//		RightPrimary.Set(y1 * MaxOutput);
//		RightSecondary.Set(y1 * MaxOutput);

//		LeftPrimary.Set(y2 * MaxOutput);
//		LeftSecondary.Set(y2 * MaxOutput);
//	}

//	public void DriveDistance(double distance)
//	{
//		double position = (distance * 12) / (4 * Math.PI);

//		RightPrimary.SetPosition(position);
//		RightSecondary.SetPosition(position);

//		LeftPrimary.SetPosition(position);
//		LeftSecondary.SetPosition(position);
//	}

//}