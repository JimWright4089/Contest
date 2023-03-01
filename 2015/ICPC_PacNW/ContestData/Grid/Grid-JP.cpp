#include<cstdio>
#include<queue>
#include<tuple>
using namespace std;
int n, m;
const int MAX = 510 ;
char a[MAX][MAX];
int d[MAX][MAX];
int dx[4]={0, -1, 0, 1};
int dy[4]={-1, 0, 1, 0};
int main()
{
	{
		scanf("%d%d", &n, &m);
		for(int i=1;i<=n;i++) scanf("%s", &a[i][1]);
		for(int i=1;i<=n;i++) for(int j=1;j<=m;j++) d[i][j]=-1;
		queue<pair<int, int>> q;
		d[1][1]=0;
		q.push(make_pair(1, 1));
		while(!q.empty())
		{
			int x, y;
			tie(x, y) = q.front(); q.pop();
			int l = a[x][y]-'0';
			for(int k=0;k<4;k++)
			{
				int nx = x+dx[k]*l;
				int ny = y+dy[k]*l;
				if(nx < 1 || ny < 1 || nx > n || ny > m) continue;
				if(d[nx][ny]!=-1) continue;
				d[nx][ny]=d[x][y]+1;
				q.push(make_pair(nx, ny));
			}
		}
		if(d[n][m]==-1) printf("IMPOSSIBLE\n");
		else printf("%d\n", d[n][m]);
	}
	return 0;
}
