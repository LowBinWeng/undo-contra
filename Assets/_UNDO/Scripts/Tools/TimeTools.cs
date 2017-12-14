using UnityEngine;
using System.Collections;
using System;
public class TimeTools : MonoBehaviour {
	
	public static string timeFormat	= "MM/dd/yyyy HH:mm:ss";



	/*==========================================================================================
	 * Conversions
	 *========================================================================================*/



	// Converts DateTime to String using set TimeFormat
	public static string ConvertDateTimeToString ( DateTime inDate ) 
	{
		return inDate.ToString( TimeTools.timeFormat );
	}



	// Converts String to DateTime using set TimeFormat
	public static DateTime ConvertStringToDateTime( string inString) 
	{
		DateTime newDate = DateTime.ParseExact( inString, TimeTools.timeFormat, System.Globalization.CultureInfo.InvariantCulture);
		return newDate;
	}



	/// <summary>
	/// Converts the seconds to string format.
	/// Format Example: "{0}h:{1}m:{2}s"
	/// Digits Example: "00" / "000"
	/// </summary>
	public static string ConvertSecondsToStringFormat(double timeInSeconds, string format, string digits) 
	{
		TimeSpan t = TimeSpan.FromSeconds( timeInSeconds );
		return string.Format( format, t.Hours.ToString(),t.Minutes.ToString(digits),t.Seconds.ToString(digits) );
	}

	public static string ConvertSecondsToPrecisionStringFormat(double timeInSeconds, string format, string digits) 
	{
		TimeSpan t = TimeSpan.FromSeconds( timeInSeconds );
		return string.Format( format,t.Minutes.ToString(digits),t.Seconds.ToString(digits), t.Milliseconds.ToString(digits) );
	}


	/*==========================================================================================
	 * Get Time durations
	 *========================================================================================*/



	// Get seconds remaining by subtracting seconds
	public static double GetTimeRemaining( double missionSecondsDuration, DateTime startDate, DateTime latestDate ) 
	{
		double timeRemaining = missionSecondsDuration - GetElapsedTimeInSeconds( startDate, latestDate ); 
		if ( timeRemaining < 0 ) timeRemaining = 0;
		return timeRemaining;
	} 



	// Get Elapsed time in Seconds (double)
	static double GetElapsedTimeInSeconds ( DateTime startDate, DateTime latestDate ) 
	{
		TimeSpan d = latestDate - startDate;
		return d.TotalSeconds;
	}



}
