#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

double cross_product(pair<double, double> p1, pair<double, double> p2)
{
	return p1.first * p2.second - p1.second * p2.first;
}

bool is_clockwise(pair<double, double> p0, pair<double, double> p1, pair<double, double> p2)
{
	pair<double, double> a = make_pair<double, double>(p1.first - p0.first, p1.second - p0.second);
	pair<double, double> b = make_pair<double, double>(p2.first - p0.first, p2.second - p0.second);
	return cross_product(a, b) < 0;
}

vector<pair<double, double>> andrew_convex_hull(vector<pair<double, double>> points)
{
	sort(points.begin(), points.end());
	vector<pair<double, double>> upper;
	vector<pair<double, double>> lower;
	upper.push_back(points[0]);
	upper.push_back(points[1]);
	lower.push_back(points[points.size() - 1]);
	lower.push_back(points[points.size() - 2]);
	for (int i = 2; i < points.size(); i++)
	{
		for (int n = upper.size(); n >= 2 && !is_clockwise(upper[n - 2], upper[n - 1], points[i]); n--)
			upper.pop_back();
		upper.push_back(points[i]);
	}
	for (int i = points.size() - 3; i >= 0; i--)
	{
		for (int n = lower.size(); n >= 2 && !is_clockwise(lower[n - 2], lower[n - 1], points[i]); n--)
			lower.pop_back();
		lower.push_back(points[i]);
	}
	for (int i = 1; i < upper.size() - 1; i++)
		lower.push_back(upper[i]);
	return lower;
}
int main()
{
	vector<pair<double, double>> points{ {2, 7},
										 {3, 5}, {3, 2}, {5, 2}, {4, 3}, {5, 5},
										 {7, 4}, {9, 4}, {7, 5}, {10, 8}, {6,9}, {4, 7}, {7, 7}, {9, 7}, {4,9} };
	for (auto x : andrew_convex_hull(points))
		cout << x.first << " " << x.second << endl;

}