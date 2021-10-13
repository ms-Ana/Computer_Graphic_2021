#include <vector>
#include <iostream>
#include <utility>

__declspec(dllexport)
int point_location(std::pair<double, double> p1, std::pair<double, double> p2, std::pair<double, double> p)
{
	auto ax = p2.first - p1.first, ay = p2.second - p1.second;
	auto bx = p.first - p1.first, by = p.second - p1.second;
	auto loc = by * ax - bx * ay;
	if (loc == 0 && (p.first <= p2.first && p.first >= p1.first || p.first <= p1.first && p.first >= p2.first))
		return 2;
	if (loc == 0)
		return 0;
	if (loc > 0)
		return -1;
	return 1;
}

__declspec(dllexport)
bool inside_polygon(const std::pair<double, double>& p,
	const std::vector<std::pair<double, double>>& points)
{
	bool answer = false;
	auto j = points.size() - 1;
	for (int i = 0; i < points.size(); i++) {
		if (point_location(points[i], points[j], p) == 2 || point_location(points[j], points[i], p) == 2)
			return true;
		if ((points[i].second < p.second && points[j].second >= p.second
			|| points[i].second >= p.second && points[j].second < p.second)
			&& (points[i].first + (p.second - points[i].second) /
				(points[j].second - points[i].second) * (points[j].first - points[i].first) < p.first)) {
			answer = !answer;
		}
		j = i;
	}
	return answer;
}