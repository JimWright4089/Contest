#include<cstdio>
#include<algorithm>
using namespace std;
int cnt[30];
int main()
{
	int i;
	char buf[128];
	scanf("%s", buf);
	for (i = 0; buf[i]; i++) cnt[buf[i] - 'a']++;
	sort(&cnt[0], &cnt[26]);
	printf("%d\n", i - (cnt[24] + cnt[25]));
	return 0;
}
