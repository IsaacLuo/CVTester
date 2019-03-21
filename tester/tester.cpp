// tester.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <opencv2/core.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/imgcodecs.hpp>
#include <fstream>
#include <sstream>
#include <cstdlib>

using namespace std;
using namespace cv;

int imageProcess(const cv::Mat& src, cv::Mat& dst, std::ostringstream& log);

int main(int argc, char *argv[])
{
	if (argc < 4) {
		std::cout << "no image file as argv[1]" << std::endl;
		return -1;
	}
	std::string fileName{ argv[1] };
	std::string dstLogName{ argv[2] };
	std::string dstImageName{ argv[3] };

	cv::Mat srcImg = cv::imread(fileName);
	cv::Mat dstImg;
	std::ostringstream oss;

	int code = imageProcess(srcImg, dstImg, oss);
	if (code == 0)
	{
		cv::imwrite(dstImageName, dstImg);
		std::ofstream ofs{ dstLogName };
		std::string log = oss.str();
		ofs.write(log.c_str(), log.length());
		ofs.close();
		std::cout << dstLogName << std::endl;
		std::cout << dstImageName << std::endl;
	}
	return code;
}

int imageProcess(const cv::Mat& src, cv::Mat& dst, std::ostringstream& log)
{
	cv::GaussianBlur(src, dst, cv::Size{ 3,3 }, 1.0, 0.0, 4);
	log << "GaussianBlur" << std::endl;
	
	int row = dst.rows / 2;
	for (int col = 0; col < dst.cols; col++) {
		cv::Vec3b& p = dst.at<cv::Vec3b>(row, col);
		p[0] = 255;
		p[1] = 0;
		p[2] = 0;
	}

	return 0;
}