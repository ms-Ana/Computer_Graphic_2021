using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
class Program {
  [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
public static extern int point_location(KeyValuePair<double, double> p1, KeyValuePair<double, double> p2, KeyValuePair<double, double> p);

 [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
  public static extern bool inside_polygon(KeyValuePair<double, double> p, 
	KeyValuePair<double, double>[] points);

  public static void Main (string[] args) {
  KeyValuePair<double, double>[] points = new KeyValuePair<double, double>[] { 
     new KeyValuePair<double, double>(4, 1), 
     new KeyValuePair<double, double>(7, 1), 
     new KeyValuePair<double, double>(5, 3), 
     new KeyValuePair<double, double>(8, 3), 
     new KeyValuePair<double, double>(5, 6), 
     new KeyValuePair<double, double>(4, 4), 
     new KeyValuePair<double, double>(1, 4)};
     KeyValuePair<double, double> p1 = new KeyValuePair<double, double>(4, 2);
     KeyValuePair<double, double> p2 = new KeyValuePair<double, double>(4, 3);
     KeyValuePair<double, double> p3 = new KeyValuePair<double, double>(3, 3);
	   KeyValuePair<double, double> p4 = new KeyValuePair<double, double>( 5, 1);
	   KeyValuePair<double, double> p5 = new KeyValuePair<double, double>( 3, 2 );
	   KeyValuePair<double, double> p6 = new KeyValuePair<double, double>( 1, 2 );
	   KeyValuePair<double, double> p7 = new KeyValuePair<double, double>( 5, 6 );
	   KeyValuePair<double, double> p8 = new KeyValuePair<double, double>( 9, 3 );
     Console.WriteLine(inside_polygon(p1, points));
	
  }
