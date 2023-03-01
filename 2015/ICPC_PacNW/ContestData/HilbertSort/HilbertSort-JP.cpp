#include<cstdio>
#include<vector>
using namespace std;
struct point
{
	int x, y; char *id;
	point() {}
	point(int x, int y, char *id) : x(x), y(y), id(id) {}
};
char ids[200100][64];
point a[200100];
int n, S;
void hsort(vector<point>& v)
{
	if (v.size() <= 1) return;
	vector<point> v0, v1, v2, v3;
	for (int i = 0; i < v.size(); i++)
	{
		if (2 * v[i].x <= S && 2 * v[i].y <= S) v0.push_back(point(v[i].y * 2, v[i].x * 2, v[i].id));
		if (2 * v[i].x <= S && 2 * v[i].y >= S) v1.push_back(point(v[i].x * 2, v[i].y * 2 - S, v[i].id));
		if (2 * v[i].x >= S && 2 * v[i].y >= S) v2.push_back(point(v[i].x * 2 - S, v[i].y * 2 - S, v[i].id));
		if (2 * v[i].x >= S && 2 * v[i].y <= S) v3.push_back(point(S - v[i].y * 2, 2 * (S - v[i].x), v[i].id));
	}
	hsort(v0), hsort(v1), hsort(v2), hsort(v3);
	int k = 0;
	for (int i = 0; i < v0.size(); i++) v[k++] = v0[i];
	for (int i = 0; i < v1.size(); i++) v[k++] = v1[i];
	for (int i = 0; i < v2.size(); i++) v[k++] = v2[i];
	for (int i = 0; i < v3.size(); i++) v[k++] = v3[i];
}
int main()
{
	scanf("%d%d", &n, &S);
	for (int i = 0; i < n; i++) { scanf("%d%d%s", &a[i].x, &a[i].y, ids[i]); a[i].id = ids[i]; }
	auto v = vector<point>(&a[0], &a[n]);
	hsort(v);
	for (int i = 0; i < n; i++) printf("%s\n", v[i].id);
	return 0;
}
