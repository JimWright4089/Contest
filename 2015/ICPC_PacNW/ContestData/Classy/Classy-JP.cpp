#include<cstdio>
#include<cstring>
#include<algorithm>
#include<vector>
#include<string>
using namespace std;
vector<string> a;
int main()
{
	int n;
	for (scanf("%d", &n); n--;)
	{
		char buf[64];
		scanf("%s", buf);
		string name(buf, strlen(buf) - 1);
		string cl;
		while (true)
		{
			char buf[64];
			scanf("%s", buf);
			if (!strcmp(buf, "class")) break;
			if (buf[0] == 'u') cl = string("a") + cl;
			if (buf[0] == 'm') cl = string("b") + cl;
			if (buf[0] == 'l') cl = string("c") + cl;
		}
		cl += string(60 - cl.length(), 'b');
		a.push_back(cl + name);
	}
	sort(a.begin(), a.end());
	for (int i = 0; i < a.size(); i++) printf("%s\n", a[i].c_str() + 60);
	return 0;
}
