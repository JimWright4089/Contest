#include<cstdio>
int n;
int a[16][2];
int main()
{
	int i;
	scanf("%d", &n);
	for (i = 1; i <= n; i++)
	{
		char buf[16];
		scanf("%s%d", buf, &a[i][1]);
		if (buf[0] == 'A') a[i][0] = 1;
		if (buf[0] == 'S') a[i][0] = 2;
		if (buf[0] == 'M') a[i][0] = 3;
		if (buf[0] == 'D') a[i][0] = 4;
	}
	int ans = 0;
	for (int k = 1; k <= 100; k++)
	{
		int c = k;
		for (i = 1; i <= n; i++)
		{
			if (a[i][0] == 1) c += a[i][1];
			else if (a[i][0] == 2 && c >= a[i][1]) c -= a[i][1];
			else if (a[i][0] == 3) c *= a[i][1];
			else if (a[i][0] == 4 && !(c%a[i][1])) c /= a[i][1];
			else break;
		}
		if (i <= n) ans++;
	}
	printf("%d\n", ans);
	return 0;
}
