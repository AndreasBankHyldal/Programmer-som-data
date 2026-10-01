void arrsum(int n, int arr[], int *sump) {
  int i;
  int sum;
  i = 0;
  sum = 0;
  while (i < n) {
    sum = sum + arr[i];
    i = i + 1;
  }
  *sump = sum;
}

void main() {
  int a[4];
  int sum;
  a[0] = 7;
  a[1] = 13;
  a[2] = 9;
  a[3] = 8;
  sum = 0;
  arrsum(4, a, &sum);
  print sum;
  println;
}