#include<cstdio>
int main()
{
	int n, k;
	int l, u;
	scanf("%d%d", &n, &k);
	l = 2; u = k - 1;
	while (n--)
	{
		int f;
		char res[16];
		scanf("%d%s", &f, res);
		if (res[0] == 'S' && l < f + 1) l = f + 1;
		if (res[0] == 'B' && u > f - 1) u = f - 1;
	}
	printf("%d %d\n", l, u);
	return 0;
}
