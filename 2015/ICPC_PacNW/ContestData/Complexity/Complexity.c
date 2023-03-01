#include <assert.h>
#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>

#define MAXN 100

// Sort integers descending.
int intcmp(const void *A, const void *B) {
  int a = *((int *)A);
  int b = *((int *)B);
  return b - a;
}

int main() {
  char s[MAXN+1];
  char *p;
  int freq[256] = {0};
  int i, n;

  assert(scanf(" %s", s) == 1);

  // Compute letter frequencies.
  for (p = s; *p; p++) {
    assert(islower(*p));
    freq[(int)*p]++;
  }

  // Sort descending.
  qsort(freq, 256, sizeof(freq[0]), intcmp);

  // Erase all but the first two.
  n = 0;
  for (i = 2; i < 256; i++) {
    n += freq[i];
  }

  printf("%d\n", n);
  return 0;
}
