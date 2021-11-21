def  cross_product(p1, p2):
	return p1[0] * p2[1] - p1[1] * p2[0]

def is_clockwise(p0, p1, p2):
	a = (p1[0] - p0[0], p1[1] - p0[1])
	b = (p2[0] - p0[0], p2[1] - p0[1])
	return cross_product(a, b) < 0

def andrew_convex_hull(points):
	points.sort()
	upper, lower = [], []
	upper.append(points[0])
	upper.append(points[1])
	lower.append(points[-1])
	lower.append(points[-2])
	for i in range(2, len(points)):
		for n  in range(len(upper),1, -1):
			if is_clockwise(upper[n - 2], upper[n - 1], points[i]):
				break
			del upper[-1]
		upper.append(points[i])
	for i in range(len(points) - 3, -1,-1):
		for n in range(len(lower), 1, -1):
			if is_clockwise(lower[n - 2], lower[n - 1], points[i]):
				break
			del lower[-1]
		lower.append(points[i])

	lower.extend(upper)
	return lower

if __name__ == '__main__':
	points = [ [2, 7],[3, 5], [3, 2], [5, 2], [4, 3], [5, 5], [7, 4], [9, 4], [7, 5], [10, 8], [6,9], [4, 7], [7, 7], [9, 7], [4,9] ]
	print(andrew_convex_hull(points))