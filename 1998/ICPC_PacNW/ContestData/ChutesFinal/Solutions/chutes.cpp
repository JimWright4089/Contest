#include <string.h>

#include <stdio.h>

#include <malloc.h>

#include <stdlib.h>



int main() {

	int i,j, k,N, extra, won;

	int board[101];

	int pos[6], miss[6];

	int throws[1000];

	FILE * inp = fopen("chutes.dat","r");



	for (i=0,j=1;j>0;j=throws[i],i++) fscanf(inp," %d",&throws[i]);

	do {

		for (i = 0;i <= 100;i++) board[i] = 0;

		for (i = 0;i < 6;i++) miss[i] = pos[i] = 0;

		fscanf(inp," %d",&N);

		if (N == 0) break;

		for (;;) {

			fscanf(inp," %d %d",&i,&j);

			if (i == 0) break;

			board[i] = 100 + j;

		}

		for (;;) {

			fscanf(inp," %d",&i);

			if (i == 0) break;

			board[abs(i)] = i/abs(i);

		}

		for (i = j = 0;;i=(++i)%N) {

			if (miss[i]) miss[i] = 0;

			else do {

				extra = won = 0;

				k = throws[j++];

				if (pos[i] + k == 100) {

					won = 1;

					break;

				}

				if (pos[i] + k < 100) {

					pos[i] += k;

					if (board[pos[i]] > 100) pos[i] =
board[pos[i]] - 100;

					else if (board[pos[i]] < 0) miss[i]
= 1;

					else if (board[pos[i]] > 0) extra = 1;

				}

			} while (extra);

//	printf("Player %d on %d\n",i,pos[i]);

			if (won) break;

		}

		printf("%d\n",i + 1);

	} while(1);

/* 	scanf(" %d",&i); */

	return 0;

}



