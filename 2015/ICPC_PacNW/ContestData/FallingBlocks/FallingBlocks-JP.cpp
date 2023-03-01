#include<cstdio>
#include<cstring>
#include<vector>
#include<map>
#include<set>
using namespace std;
char blocks[32]; int n;
int a[32];
const int B = 8, BB = 64;
int btoi[8][4] = {
	{2 + 2 * B + 7 * BB, 1 + 7 * B + 1 * BB, 7 + 2 * B + 2 * BB, 4 + 7 * B + 4 * BB}, // T
	{2 + 7 * B + 2 * BB, 2 + 7 * B + 2 * BB, 2 + 7 * B + 2 * BB, 2 + 7 * B + 2 * BB}, // X
	{2 + 6 * B + 3 * BB, 1 + 7 * B + 2 * BB, 6 + 3 * B + 2 * BB, 2 + 7 * B + 4 * BB}, // R
	{6 + 2 * B + 3 * BB, 1 + 7 * B + 4 * BB, 6 + 2 * B + 3 * BB, 1 + 7 * B + 4 * BB}, // S
	{3 + 2 * B + 6 * BB, 4 + 7 * B + 1 * BB, 3 + 2 * B + 6 * BB, 4 + 7 * B + 1 * BB}, // Z
	{7 + 4 * B + 4 * BB, 4 + 4 * B + 7 * BB, 1 + 1 * B + 7 * BB, 7 + 1 * B + 1 * BB}, // V
	{2 + 3 * B + 6 * BB, 2 + 7 * B + 1 * BB, 3 + 6 * B + 2 * BB, 4 + 7 * B + 2 * BB}, // 7
	{3 + 6 * B + 4 * BB, 4 + 6 * B + 3 * BB, 1 + 3 * B + 6 * BB, 6 + 3 * B + 1 * BB} // W
};
char bch[] = "TXRSZV7W";
const int top3 = 511 << 21; // mask for top 3 rows of the board
vector<int> next_boards(int cur, int x) // given block x
{
	vector<int> ret;
	if (cur&top3) return ret;
	for (int rot = 0; rot < 4; rot++)
	{
		int t = btoi[x][rot] << 21;
		// block falls until touching ground, or any block
		while (!(t & 7) && !((t >> 3)&(cur))) t >>= 3;
		int nxt = cur | t;
		for (int i = 0, r=7; i < 10; i++, r<<=3)
		{
			while ((nxt&r) == r) // remove ith row if full
			{
				int b1=nxt%(1<<(3*i)), b2=(nxt>>(3*(i+1)))<<(3*i);
				nxt = b1 | b2;
			}
		}
		bool flag = false;
		for (int i = 0; i < ret.size(); i++)
		{
			if (ret[i] == nxt) { flag = true; break; }
		}
		if(!flag) ret.push_back(nxt);
	}
	return ret;
}
map<int, int> d[32];
set<int> visited[32];
int get_count(int cur, int k) // to drop kth block
{
	if (visited[k].count(cur)) return -1;
	auto it = d[k].find(cur);
	if (it != d[k].end()) return it->second;
	visited[k].insert(cur);
	vector<int> nxts = next_boards(cur, a[k]);
	int ret = 0;
	for (auto nxt : nxts)
	{
		int ncnt = get_count(nxt, (k + 1) % n);
		if (ncnt == -1) return -1;
		if (ret < ncnt + 1) ret = ncnt + 1;
	}
	d[k][cur] = ret;
	visited[k].erase(cur);
	return ret;
}
int main()
{
	scanf("%s", blocks);
	n = strlen(blocks);
	for (int i = 0; i < n; i++)
	{
		int j;
		for (j = 0; bch[j] != blocks[i]; j++);
		a[i] = j;
	}
	int ans = get_count(0, 0);
	if (ans == -1) printf("forever\n");
	else printf("%d\n", ans);
	return 0;
}
